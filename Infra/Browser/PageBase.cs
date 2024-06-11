using System.Threading.Tasks;
using Microsoft.Playwright;

public abstract class PageBase
{
    protected IPage _page;

    protected PageBase(IPage page)
    {
        _page = page;
    }

    public abstract string PageUrl { get; }

    public IPage Page => _page;

    public string BaseUrl => ConfigProvider.WEBSITE_BASE_URL;

    public async Task NavigateAsync()
    {
        await _page.GotoAsync(PageUrl);
    }

    public async Task WaitForLoadStateAsync()
    {
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task WaitForLoadAsync()
    {
        await _page.WaitForLoadStateAsync(LoadState.Load);
    }

    public async Task WaitForLoadNetworkAsync()
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task WaitForElementAsync(string selector, int timeout = 30000)
    {
        await _page.Locator(selector).WaitForAsync(new LocatorWaitForOptions { Timeout = timeout });
    }

    public async Task WaitForElementToBeVisibleAsync(string selector, int timeout = 30000)
    {
        await _page.Locator(selector).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = timeout });
    }

    public async Task WaitForElementToBeHiddenAsync(string selector, int timeout = 30000)
    {
        await _page.Locator(selector).WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden, Timeout = timeout });
    }

    public async Task WaitForElementAsync(ILocator locator, int timeout = 30000)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions { Timeout = timeout });
    }

    public async Task WaitForElementToBeVisibleAsync(ILocator locator, int timeout = 30000)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = timeout });
    }

    public async Task WaitForElementToBeHiddenAsync(ILocator locator, int timeout = 30000)
    {
        await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Hidden, Timeout = timeout });
    }
}