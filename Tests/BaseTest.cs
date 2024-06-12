using Microsoft.Playwright.NUnit;
using Serilog;

/// <summary>
/// Provides a base test class for setting up and tearing down global and per-test contexts,
/// integrating plugins, and handling tracing and screenshot capture with Playwright and Serilog in NUnit tests.
/// </summary>
[TestFixture]
public abstract class BaseTest : PageTest
{
    protected ILogger _logger;
    protected WorkerContext _workerContext;
    protected TestContext _testContext;
    protected static List<IWorkerPlugin> workerPlugins = new List<IWorkerPlugin>();
    protected static List<ITestPlugin> testPlugins = new List<ITestPlugin>();

    /// <summary>
    /// Sets up the global context before any tests run.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        _logger = Logger.Instance;
        _logger.Information("*** Global worker fixture setup ***");

        _workerContext = new WorkerContext();

        foreach (var plugin in workerPlugins)
        {
            await plugin.RunBeforeWorker(_workerContext);
        }
    }

    /// <summary>
    /// Tears down the global context after all tests have run.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        _logger.Information("*** Global worker fixture teardown ***");

        foreach (var plugin in workerPlugins)
        {
            await plugin.RunAfterWorker(_workerContext);
        }

        if (_workerContext != null)
        {
            await _workerContext.DisposeAsync();
        }
    }

    /// <summary>
    /// Sets up the context for each test.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [SetUp]
    public async Task TestSetup()
    {
        _logger.Information("*** Before Each ***");

        var testName = NUnit.Framework.TestContext.CurrentContext.Test.Name;
        _testContext = new TestContext(_workerContext, testName, _logger);

        await Context.Tracing.StartAsync(new()
        {
            Title = $"{NUnit.Framework.TestContext.CurrentContext.Test.Name}",
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        foreach (var plugin in testPlugins)
        {
            await plugin.RunBeforeTest(_workerContext, _testContext, NUnit.Framework.TestContext.CurrentContext);
        }
    }

    /// <summary>
    /// Tears down the context for each test.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [TearDown]
    public async Task TestTeardown()
    {
        _logger.Information("*** After Each ***");

        var failed = NUnit.Framework.TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Error
            || NUnit.Framework.TestContext.CurrentContext.Result.Outcome == NUnit.Framework.Interfaces.ResultState.Failure;

        await Context.Tracing.StopAsync(new()
        {
            Path = failed ? Path.Combine(
                AppContext.BaseDirectory, @"../../../Resources/playwright-traces",
                $"{NUnit.Framework.TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.zip"
            ) : null,
        });

        // Capture screenshot if the test failed
        if (failed) await _testContext.TakeScreenshotsAsync(NUnit.Framework.TestContext.CurrentContext.Test.Name);

        foreach (var plugin in testPlugins)
        {
            await plugin.RunAfterTest(_workerContext, _testContext, NUnit.Framework.TestContext.CurrentContext);
        }

        if (_testContext != null)
        {
            await _testContext.TearDownAsync();
            await _testContext.DisposeAsync();
        }
    }
}