package org.miguel.elastic;

import lombok.SneakyThrows;
import lombok.extern.slf4j.Slf4j;
import org.apache.http.HttpHost;
import org.apache.http.auth.AuthScope;
import org.apache.http.auth.UsernamePasswordCredentials;
import org.apache.http.client.CredentialsProvider;
import org.apache.http.impl.client.BasicCredentialsProvider;
import org.elasticsearch.action.index.IndexRequest;
import org.elasticsearch.action.index.IndexResponse;
import org.elasticsearch.client.RequestOptions;
import org.elasticsearch.client.RestClient;
import org.elasticsearch.client.RestClientBuilder;
import org.elasticsearch.client.RestHighLevelClient;
import org.elasticsearch.common.xcontent.XContentType;

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

    @SneakyThrows
    public static void main(String[] args) {
        RestHighLevelClient client = createClient();

        String jsonString = "{ \"foo\": \"bar4\" }";

        var indexRequest = new IndexRequest(
                "twitter"
        ).source(jsonString, XContentType.JSON);

        IndexResponse indexResponse = client.index(indexRequest, RequestOptions.DEFAULT);
        String id = indexResponse.getId();
        log.info(id);

        // close the client gracefully
        client.close();
    }
}
