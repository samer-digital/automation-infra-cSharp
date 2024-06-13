using Microsoft.Playwright;

public class GoogleMapsMobilePage : PageBase
{
    private ILocator _menuOpenBtn;
    private ILocator _searchInput;
    private ILocator _directionButton;
    private ILocator _currentLocationBtn;
    private ILocator _keepUsingWebBtn;
    private ILocator _inputClickable;
    private ILocator _errorMessage;

    public GoogleMapsMobilePage(IPage page) : base(page)
    {
        _keepUsingWebBtn = _page.Locator("button[class='vrdm1c K2FXnd Oz0bd oNZ3af']");
        _menuOpenBtn = _page.Locator("button[aria-label='Menu']");
        _directionButton = _page.Locator("button[class='S5LAUe iNL5bb OqjlOd']");
        _currentLocationBtn = _page.Locator("button[class='uWaeI G7ZKsf']");
        _inputClickable = _page.Locator("div[class='NtcBjb R30LOe eD6Rpc GnJVlc']");
        _searchInput = _page.Locator("#ml-searchboxinput");
        _errorMessage = _page.Locator("div[class='j9ygdd']");
    }

    public override string PageUrl => BaseUrl;

    public SearchResultsMobileComponent GetSearchResultsComponent()
    {
        return new SearchResultsMobileComponent(this);
    }

    public async Task ClickKeepUsingWebBtn()
    {
        await _keepUsingWebBtn.ClickAsync();
    }

    public async Task ClickTheSearchInput()
    {
        await _inputClickable.ClickAsync();
    }

    public async Task FillSearchAsync(string searchTerm)
    {
        await ClickTheSearchInput();
        await _searchInput.FillAsync(searchTerm);
        await KeyboardPress("Enter");
    }

    public async Task<string?> IsNoResultPopupVisible()
    {
        return await _errorMessage.TextContentAsync();
    }

    public async Task ClickToOpenMenu()
    {
        await _menuOpenBtn.ClickAsync();
    }

    public async Task ClickDirectionBtn()
    {
        await _directionButton.ClickAsync();
    }

    public async Task ClickOnGetCurrentLocationBtn()
    {
        await _currentLocationBtn.ClickAsync();
    }
}