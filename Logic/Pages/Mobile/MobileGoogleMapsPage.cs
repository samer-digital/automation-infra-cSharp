using Microsoft.Playwright;

public class GoogleMapsMainMobilePage : GoogleMapsMenuPageBase
{
    private ILocator SearchInput;
    private ILocator CurrentLocationBtn;
    private ILocator ErrorMessage;
    private ILocator KeepUsingWebButton;
    private ILocator SearchInputClickable;

    

    // I already have nightmares, locators in google maps are so bad beacuse they different also in other langauges!!!!
    public GoogleMapsMainMobilePage(IPage page) : base(page)
    {
        CurrentLocationBtn = _page.Locator("button[aria-label*='map'][aria-checked='false']"); 
        SearchInput = _page.Locator("#ml-searchboxinput");
        ErrorMessage = _page.Locator("div[class='j9ygdd']");
        KeepUsingWebButton = _page.GetByText("Keep using web");
        SearchInputClickable = _page.GetByText("Find a place");
    }

    public override string PageUrl => BaseUrl;

    public SearchResultsMobileComponent GetSearchResultsComponent()
    {
        return new SearchResultsMobileComponent(this);
    }

    public async Task ClickKeepUsingWebBtn()
    {
        await KeepUsingWebButton.ClickAsync();
    }

    public async Task ClickTheSearchInput()
    {
        await SearchInputClickable.ClickAsync();
    }

    public async Task FillSearchAsync(string searchTerm)
    {
        await ClickTheSearchInput();
        await SearchInput.FillAsync(searchTerm);
        await KeyboardPress("Enter");
    }

    public async Task<string?> IsNoResultPopupVisible()
    {
        return await ErrorMessage.TextContentAsync();
    }

    public async Task ClickOnGetCurrentLocationBtn()
    {
        await CurrentLocationBtn.ClickAsync();
    }
}