using System.Diagnostics;

namespace WorkerServiceSample
{
    public class Worker : BackgroundService
    {
        private static int _count;
        private readonly ILogger<Worker> _logger;
        private readonly PeriodicTimer _timer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _timer = new(TimeSpan.FromSeconds(3));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Stopwatch quickStopwatch = new();
                    Stopwatch slowStopwatch = new();

                    var quickTask = Task.Run(async () =>
                    {
                        quickStopwatch.Start();
                        await DoQuickWork(stoppingToken);
                        quickStopwatch.Stop();
                    }, stoppingToken);

                    var slowTask = Task.Run(async () =>
                    {
                        slowStopwatch.Start();
                        await DoSlowWork(stoppingToken);
                        slowStopwatch.Stop();
                    }, stoppingToken);

                    await Task.WhenAll(quickTask, slowTask);

                    _logger.LogInformation("Quick task finished in: {time} ms", quickStopwatch.ElapsedMilliseconds);
                    _logger.LogInformation("Slow task finished in: {time} ms", slowStopwatch.ElapsedMilliseconds);


                    await _timer.WaitForNextTickAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in Worker");
                }
            }
        }

        private async Task DoQuickWork(CancellationToken stoppingToken)
        {
            await Task.Delay(500, stoppingToken);
        }

        private async Task DoSlowWork(CancellationToken stoppingToken)
        {
            await Task.Delay(2000, stoppingToken);
        }
    }
}
