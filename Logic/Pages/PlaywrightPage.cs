using Microsoft.Playwright;

public class PlaywrightPage : PageBase
{
    private ILocator _navLogoLocator;
    private ILocator _searchLocator;
    private ILocator _searchInputLocator;
    private ILocator _resetSearchLocator;

    public PlaywrightPage(IPage page) : base(page)
    {
        _navLogoLocator = page.Locator(".navbar__logo");
        _searchLocator = page.Locator(".DocSearch-Button-Placeholder");
        _searchInputLocator = page.Locator(".DocSearch-Input");
        _resetSearchLocator = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Clear the query" });
    }

    public override string PageUrl => BaseUrl;

    public ILocator NavLogoLocator => _navLogoLocator;

    public ILocator ResetSearchLocator => _resetSearchLocator;

    public async Task FillSearchAsync(string searchTerm)
    {
        await _searchInputLocator.FillAsync(searchTerm);
        await Page.WaitForTimeoutAsync(3000);
    }

    public async Task ClickSearchAsync()
    {
        await _searchLocator.ClickAsync();
    }
}