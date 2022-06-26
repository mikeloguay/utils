package org.miguel.tutorial1;

import lombok.extern.slf4j.Slf4j;

import java.util.concurrent.CountDownLatch;

@Slf4j
public class ConsumerDemoWithThread {
    public static void main(String[] args) {
        new ConsumerDemoWithThread().run();
    }

    private ConsumerDemoWithThread() {
    }

    private void run() {
        String bootstrapServers = "localhost:9092";
        String groupId = "my-sixth-app";
        String topic = "my_topic";

        // latch for dealing with multiple threads
        var latch = new CountDownLatch(1);

        // create the consumer runnable
        Runnable myConsumerRunnable = new ConsumerRunnable(bootstrapServers, groupId, topic, latch);

        // start the thread
        var myThread = new Thread(myConsumerRunnable);
        myThread.start();

        // add a shutdown hook
        Runtime.getRuntime().addShutdownHook(new Thread( () ->{
            log.info("Caught shutdown hook");
            ((ConsumerRunnable)myConsumerRunnable).shutdown();
            try {
                latch.await();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
            log.info("Application has exited");
        }
        ));

        try {
            latch.await();
        } catch (InterruptedException e) {
            log.error("Application got interrupted", e);
        } finally {
            log.info("Application is closing...");
        }
    }
}
