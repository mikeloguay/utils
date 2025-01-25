using System.Diagnostics.Metrics;

namespace OtelAspNetSample;

public class Instrumentation : IDisposable
{
    const string METER_NAME = "Mikelus.OtelAspNetSample";
    private readonly Meter _meter;

    public Instrumentation()
    {
        string? version = typeof(Instrumentation).Assembly.GetName().Version?.ToString();
        _meter = new Meter(METER_NAME, version);
        MfgCounter = _meter.CreateCounter<long>("mfg_counter", description: "The number of whatever mfgs");
    }

    public Counter<long> MfgCounter { get; }

    public void Dispose()
    {
        _meter.Dispose();
    }
}
