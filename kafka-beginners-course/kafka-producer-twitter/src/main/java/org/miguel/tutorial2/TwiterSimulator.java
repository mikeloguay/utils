package org.miguel.tutorial2;

import java.util.concurrent.BlockingQueue;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;

import static java.util.concurrent.TimeUnit.MINUTES;
import static java.util.concurrent.TimeUnit.SECONDS;

public class TwiterSimulator {
    private final ScheduledExecutorService scheduler = Executors.newScheduledThreadPool(1);
    private static int count;

    public void writeMessageInQue(BlockingQueue<String> msgQueue) {
        Runnable twitterWriter = () -> msgQueue.add("twitter message  " + count++);
        ScheduledFuture<?> handler =
                scheduler.scheduleAtFixedRate(twitterWriter, 1, 2, SECONDS);
        Runnable canceller = () -> handler.cancel(false);
        scheduler.schedule(canceller, 1, MINUTES);
    }
}
