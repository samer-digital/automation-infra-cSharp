using Microsoft.Playwright;

public class SearchResultsComponent : ComponentBase
{

    public SearchResultsComponent(PageBase pageBase) : base(pageBase)
    {
        ShareLocationBtn = _page.Locator("button[data-value='Share']");
    }

    private ILocator CantFindLocationText => _page.Locator("div[class='Q2vNVc']");
    private ILocator LocationTitle => _page.Locator("h1[class='DUwDvf lfPIob']");
    private ILocator SearchBox => _page.Locator("#searchbox");
    private ILocator CloseBtn => SearchBox.Locator("button[aria-label='Close']");

    private ILocator CloseSharehPopupBtn => _page.Locator("button[jsaction='modal.close']");
    private ILocator CopyLinkBtn => _page.GetByText("Copy link");
    private ILocator LinkToShare => _page.Locator("input[class='vrsrZe']");
    private ILocator ShareLocationBtn;

    public async Task ClickSharehLocationBtn()
    {
        await ShareLocationBtn.ClickAsync();
    }

    public async Task ClickCloseBtn()
    {
        await CloseBtn.ClickAsync();
    }

    public async Task<string?> getLinkToShare() 
    {
        return await LinkToShare.GetAttributeAsync("value");
    }

    public async Task ClickCloseSharehPopupBtn()
    {
        await CloseSharehPopupBtn.ClickAsync();
    }

    public async Task ClickCopyLinkBtn() 
    {
        await CopyLinkBtn.ClickAsync();
    }

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