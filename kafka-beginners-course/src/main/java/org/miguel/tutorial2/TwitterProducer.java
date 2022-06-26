package org.miguel.tutorial2;

import lombok.extern.slf4j.Slf4j;
import org.apache.kafka.clients.producer.*;
import org.apache.kafka.common.serialization.StringSerializer;

import java.util.Properties;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;

@Slf4j
public class TwitterProducer {

    private String consumerKey  = "LCx8wS0R3CD0X0GThxjAXYBwk";
    private String consumerSecret = "JazBYiEP8YV4n8zlricyn7LfvfIrbEZ6lGXIEXAPTu6jIT4fS6";
    private String token = "213801753-AtdNFzqE0yy1kMGoqUB602PO6BULW3PJ07cb5esE";
    private String secret = "oNFne4UxEVpW6XpJkvIKotGWx88VyGkxXN1f20VucME42";

    private TwitterProducer() {
    }

    public static void main(String[] args) {
        new TwitterProducer().run();
    }

    public void run() {
        log.info("Setup");

        // create twitter client
        /* Set up your blocking queues: Be sure to size these properly based on expected TPS of your stream */
        BlockingQueue<String> msgQueue = new LinkedBlockingQueue<String>(100000);
        var twitterSimulator = new TwiterSimulator();
        twitterSimulator.writeMessageInQue(msgQueue);

        // create a kafka producer
        KafkaProducer<String, String> producer = createKafkaProducer();

        // add a shutdown hook
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            log.info("Stopping application...");
            log.info("closing producer...");
            producer.close();
            log.info("done!");
        }));

        // loop to send tweets to Kafka
        // on a different thread, or multiple different threads....
        while (true) {
            String msg = null;
            try {
                msg = msgQueue.poll(5, TimeUnit.SECONDS);
            } catch (InterruptedException e) {
                log.error("Error with polling");
                e.printStackTrace();
            } catch (RuntimeException e) {
                log.error("Unexpected error");
                e.printStackTrace();
            }

            if (msg != null) {
                log.info(msg);
                producer.send(new ProducerRecord<>("twitter_tweets", null, msg), new Callback() {
                    @Override
                    public void onCompletion(RecordMetadata metadata, Exception exception) {
                        if (exception != null) {
                            log.error("Something bad happened", exception);
                        }
                    }
                });
            }
        }
    }

    private KafkaProducer<String, String> createKafkaProducer() {
        String bootstrapServers = "localhost:9092";

        // create Producer properties
        var properties = new Properties();
        properties.setProperty(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);
        properties.setProperty(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, StringSerializer.class.getName());
        properties.setProperty(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, StringSerializer.class.getName());

        // create safe producer
        properties.setProperty(ProducerConfig.ENABLE_IDEMPOTENCE_CONFIG, "true");
        properties.setProperty(ProducerConfig.ACKS_CONFIG, "all");
        properties.setProperty(ProducerConfig.RETRIES_CONFIG, Integer.toString(Integer.MAX_VALUE));
        properties.setProperty(ProducerConfig.MAX_IN_FLIGHT_REQUESTS_PER_CONNECTION, "5");

        // high throughput producer
        properties.setProperty(ProducerConfig.COMPRESSION_TYPE_CONFIG, "snappy");
        properties.setProperty(ProducerConfig.LINGER_MS_CONFIG, "20");
        properties.setProperty(ProducerConfig.BATCH_SIZE_CONFIG, Integer.toString(32*1024));

        // create the producer
        var producer = new KafkaProducer<String, String>(properties);
        return producer;
    }


}
