package org.miguel.elastic;

import com.google.gson.JsonParser;
import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.apache.http.HttpHost;
import org.apache.http.auth.AuthScope;
import org.apache.http.auth.UsernamePasswordCredentials;
import org.apache.http.client.CredentialsProvider;
import org.apache.http.impl.client.BasicCredentialsProvider;
import org.apache.kafka.clients.consumer.ConsumerConfig;
import org.apache.kafka.clients.consumer.ConsumerRecord;
import org.apache.kafka.clients.consumer.ConsumerRecords;
import org.apache.kafka.clients.consumer.KafkaConsumer;
import org.apache.kafka.common.serialization.StringDeserializer;
import org.elasticsearch.action.bulk.BulkRequest;
import org.elasticsearch.action.bulk.BulkResponse;
import org.elasticsearch.action.index.IndexRequest;
import org.elasticsearch.action.index.IndexResponse;
import org.elasticsearch.client.RequestOptions;
import org.elasticsearch.client.RestClient;
import org.elasticsearch.client.RestClientBuilder;
import org.elasticsearch.client.RestHighLevelClient;
import org.elasticsearch.common.xcontent.XContentType;

import java.time.Duration;
import java.util.Arrays;
import java.util.Properties;

@Slf4j
public class ElasticSearchConsumer {

    public static RestHighLevelClient createClient() {

        // https://afdrreedmf:qej8cuj8fw@kafka-beginners-test-7300471039.eu-central-1.bonsaisearch.net:443
        String hostname = "kafka-beginners-test-7300471039.eu-central-1.bonsaisearch.net";
        String username = "afdrreedmf";
        String password = "qej8cuj8fw";

        // don't do if you run a local ES
        final CredentialsProvider credentialsProvider = new BasicCredentialsProvider();
        credentialsProvider.setCredentials(AuthScope.ANY, new UsernamePasswordCredentials(username, password));

        RestClientBuilder builder = RestClient.builder(new HttpHost(hostname, 443, "https"))
                .setHttpClientConfigCallback(
                        httpAsyncClientBuilder -> httpAsyncClientBuilder.setDefaultCredentialsProvider(credentialsProvider));

        return new RestHighLevelClient(builder);
    }

    public static KafkaConsumer<String, String> createConsume(String topic){
        String bootstrapServers = "localhost:9092";
        String groupId = "kafka-demo-elasticsearch";

        // create consumer configs
        var properties = new Properties();
        properties.setProperty(ConsumerConfig.BOOTSTRAP_SERVERS_CONFIG, bootstrapServers);
        properties.setProperty(ConsumerConfig.KEY_DESERIALIZER_CLASS_CONFIG, StringDeserializer.class.getName());
        properties.setProperty(ConsumerConfig.VALUE_DESERIALIZER_CLASS_CONFIG, StringDeserializer.class.getName());
        properties.setProperty(ConsumerConfig.GROUP_ID_CONFIG, groupId);
        properties.setProperty(ConsumerConfig.AUTO_OFFSET_RESET_CONFIG, "earliest");
        properties.setProperty(ConsumerConfig.ENABLE_AUTO_COMMIT_CONFIG, "false");
        properties.setProperty(ConsumerConfig.MAX_POLL_RECORDS_CONFIG, "5");

        // create consumer
        var consumer = new KafkaConsumer<String, String>(properties);
        consumer.subscribe(Arrays.asList(topic));

        return consumer;
    }

    @SneakyThrows
    public static void main(String[] args) {
        RestHighLevelClient client = createClient();

        KafkaConsumer<String, String> consumer = createConsume("twitter_tweets");

        // poll for new data
        while(true) {
            ConsumerRecords<String, String> records = consumer.poll(Duration.ofMillis(100)); // new in Kafka 2.0.0

            int numRecords = records.count();
            log.info("Received {} records", numRecords);

            var bulkRequest = new BulkRequest();

            for (ConsumerRecord<String, String> record: records) {
                // where we insert data into Elasticsearch
                String jsonString = record.value();

                String id = extractIdFromMessage(jsonString);

                var indexRequest = new IndexRequest(
                        "twitter"
                ).source(jsonString, XContentType.JSON);
                indexRequest.id(id);

                bulkRequest.add(indexRequest);

                //IndexResponse indexResponse = client.index(indexRequest, RequestOptions.DEFAULT);
                //log.info(indexResponse.getId());

                //Thread.sleep(1000);
            }

            if (numRecords > 0) {
                BulkResponse bulkResponse = client.bulk(bulkRequest, RequestOptions.DEFAULT);
                log.info("Committing offsets...");
                consumer.commitSync();
                log.info("Offsets have been committed");
                Thread.sleep(1000);
            }
        }

        // close the client gracefully
        //client.close();
    }

    private static String extractIdFromMessage(String jsonString) {
        return JsonParser.parseString(jsonString)
                .getAsJsonObject()
                .get("id")
                .getAsString();
    }
}
