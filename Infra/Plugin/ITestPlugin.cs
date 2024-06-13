
public interface ITestPlugin
{
    Task RunBeforeTest(WorkerContext workerContext, TestContext testContext, NUnit.Framework.TestContext testInfo);
    Task RunAfterTest(WorkerContext workerContext, TestContext testContext, NUnit.Framework.TestContext testInfo);
}