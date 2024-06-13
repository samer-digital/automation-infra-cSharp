using Microsoft.Playwright;

public class GoogleMapsApi : ApiBase
{
    private readonly ApiExecutor _apiExecutor;

    public GoogleMapsApi(IAPIRequestContext apiContext) : base(apiContext)
    {
        _apiExecutor = new ApiExecutor(apiContext);
    }

    public override string ApiEndpointUrl => "directions/json";

    public async Task<BaseApiResponse<DirectionsResponse>> GetDirectionsAsync(string origin, string destination, string apiKey)
    {
        var url = $"{BaseUrl}{ApiEndpointUrl}?origin={origin}&destination={destination}&key={apiKey}";
        return await _apiExecutor.GetAsync<DirectionsResponse>(url);
    }
}
