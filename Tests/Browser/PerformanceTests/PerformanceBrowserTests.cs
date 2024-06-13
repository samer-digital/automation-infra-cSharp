namespace Browser;

[Parallelizable(ParallelScope.Self)]
[TestFixture, Description("Google Maps Performance Tests"), Category("Performance")]
public class PerformanceBrowserTests : BaseTest
{
    [Test, Category("LoadTime"), Description("Test the load time of the Google Maps page.")]
    public async Task TestLoadTime()
    {
        var googleMapsPage = await _testContext.GetPageAsync(page => new GoogleMapsMainPage(page), new GetPageOptions { ShouldNavigate = false });

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        await googleMapsPage.NavigateAsync();
        await googleMapsPage.WaitForLoadNetworkAsync();
        stopwatch.Stop();

        var loadTime = stopwatch.ElapsedMilliseconds;
        _logger.Information($"Load time: {loadTime} ms");

        // Assert that the load time is within acceptable limits
        var maxLoadTime = 6000;
        Assert.That(loadTime, Is.LessThanOrEqualTo(maxLoadTime), $"Load time exceeded the threshold. Actual load time: {loadTime} ms");
    }
}
