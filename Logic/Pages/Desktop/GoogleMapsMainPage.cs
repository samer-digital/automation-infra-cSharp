using Microsoft.Playwright;

public class GoogleMapsMainPage : GoogleMapsMenuPageBase
{
    private ILocator SearchInputLocator;
    private ILocator SearchButton;
    private ILocator CurrentLocationBtn;

    public GoogleMapsMainPage(IPage page) : base(page)
    {
        SearchInputLocator = page.Locator(".searchboxinput");
        SearchButton = page.Locator("#searchbox-searchbutton");
        CurrentLocationBtn = page.Locator(".sVuEFc");
    }



    public override string PageUrl => BaseUrl;


    public SearchResultsComponent GetSearchResultsComponent()
    {
        return new SearchResultsComponent(this);
    }

    public DirectionComponent GetDirectionComponent()
    {
        return new DirectionComponent(this);
    }

    public async Task FillSearchAsync(string searchTerm)
    {
        await SearchInputLocator.FillAsync(searchTerm);
    }

    public async Task ClickOnGetCurrentLocationBtn()
    {
        await CurrentLocationBtn.ClickAsync();
    }

    public async Task ClickSearchButton()
    {
        await SearchButton.ClickAsync();
    }
}