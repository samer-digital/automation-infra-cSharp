using System.Threading.Tasks;
using Serilog;

public class LoggingWorkerPlugin : IWorkerPlugin
{
    private readonly ILogger _logger;

    public LoggingWorkerPlugin(ILogger logger)
    {
        _logger = logger;
    }

    public async Task RunBeforeWorker(WorkerContext workerContext)
    {
        _logger.Information("*** Global worker setup (before) ***");
        await Task.CompletedTask;
    }

    public async Task RunAfterWorker(WorkerContext workerContext)
    {
        _logger.Information("*** Global worker teardown (after) ***");
        await Task.CompletedTask;
    }
}