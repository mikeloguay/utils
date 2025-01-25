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
                    if (_count++ % 5 == 0) throw new InvalidOperationException("MFG error every 5");
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    //await Task.Delay(1000, stoppingToken);
                    await _timer.WaitForNextTickAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in Worker");
                }
            }
        }
    }
}
