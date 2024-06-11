using Serilog;

public class LoggingTestPlugin : ITestPlugin
{
    private readonly ILogger _logger;

    public LoggingTestPlugin(ILogger logger)
    {
        _logger = logger;
    }

    public async Task RunBeforeTest(WorkerContext workerContext, TestContext testContext, NUnit.Framework.TestContext testInfo)
    {
        _logger.Information($"*** Test setup (before) for {testInfo.Test.Name} ***");
        await Task.CompletedTask;
    }

    public async Task RunAfterTest(WorkerContext workerContext, TestContext testContext, NUnit.Framework.TestContext testInfo)
    {
        _logger.Information($"*** Test teardown (after) for {testInfo.Test.Name} ***");
        await Task.CompletedTask;
    }
}