using Microsoft.Playwright;

public abstract class ApiBase
{
    protected readonly IAPIRequestContext ApiContext;

    protected ApiBase(IAPIRequestContext apiContext)
    {
        ApiContext = apiContext;
    }

    public abstract string ApiEndpointUrl { get; }

    public string BaseUrl => ConfigProvider.API_BASE_URL;
}
