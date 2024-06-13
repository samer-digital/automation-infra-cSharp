using Microsoft.Playwright;

public class SearchResultsComponent : ComponentBase
{

    public SearchResultsComponent(PageBase pageBase) : base(pageBase) { }

    private ILocator CantFindLocationText => _page.Locator("div[class='Q2vNVc']");
    private ILocator LocationTitle => _page.Locator("h1[class='DUwDvf lfPIob']");

    
    public async Task<string?> GetLocationTitle()
    {
        return await LocationTitle.TextContentAsync();
    }

    public async Task WaitForLocationTitleToBeVisible()
    {
        await PageBase.WaitForElementToBeVisibleAsync(LocationTitle);
    }

    public async Task WaitForErrorToBeVisible()
    {
        await PageBase.WaitForElementToBeVisibleAsync(CantFindLocationText);
    }

    public async Task<string?> GetErrorText()
    {
        return await CantFindLocationText.TextContentAsync();
    }
}