using Microsoft.Playwright;

public class SearchResultsComponent : ComponentBase
{

    public SearchResultsComponent(PageBase pageBase) : base(pageBase) { }

    private ILocator _cantFindLocationText => _page.Locator("div[class='Q2vNVc']");
    private ILocator _locationTitle => _page.Locator("h1[class='DUwDvf lfPIob']");
    public async Task<string?> GetLocationTitle()
    {
        return await _locationTitle.TextContentAsync();
    }

    public async Task WaitForLocationTitleToBeVisible()
    {
        await PageBase.WaitForElementToBeVisibleAsync(_locationTitle);
    }

    public async Task WaitForErrorToBeVisible()
    {
        await PageBase.WaitForElementToBeVisibleAsync(_cantFindLocationText);
    }

    public async Task<string?> GetErrorText()
    {
        return await _cantFindLocationText.TextContentAsync();
    }
}