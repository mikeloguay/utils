package org.miguel.tutorial1;

import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.apache.kafka.clients.producer.*;
import org.apache.kafka.common.serialization.StringSerializer;

import java.util.Properties;

@Slf4j
public class ProducerDemoKeys
{
    @SneakyThrows
    public static void main(String[] args )
    {
        String bootstrapServers = "localhost:9092";

        // create Producer properties
        var properties = new Properties();
        properties.setProperty(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);
        properties.setProperty(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, StringSerializer.class.getName());
        properties.setProperty(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, StringSerializer.class.getName());

        // create the producer
        var producer = new KafkaProducer<String, String>(properties);

        for (int i = 0; i < 10; i++) {

            String topic = "my_topic";
            String value = "hello world " + i;
            String key = "id_" + i;

            // create a producer record
            var record = new ProducerRecord<String, String>(topic, key, value);

            log.info("Key: " + key);

            // send data - asynchronous
            producer.send(record, new Callback() {
                @Override
                public void onCompletion(RecordMetadata recordMetadata, Exception e) {
                    // executes every time a record is successfully sent or an exception is thrown
                    if (e == null) {
                        // the record was successfully sent
                        log.info("Received new metadata. \n" +
                                "Topic: " + recordMetadata.topic() + "\n" +
                                "Partition: " + recordMetadata.partition() + "\n" +
                                "Offset: " + recordMetadata.offset() + "\n" +
                                "Timestamp: " + recordMetadata.timestamp());

                    } else {
                        log.error("Error while producing", e);
                    }
                }
            }).get(); // MAKE IT SYNCHRONOUS - BAD PRACTICE, JUST FOR DEMO
        }

        // flush data
        producer.flush();

        // flush and close producer
        producer.close();
    }
}
