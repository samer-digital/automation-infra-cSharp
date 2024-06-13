namespace API;

[TestFixture, Description("Google Maps API Tests"), Category("API")]
public class GoogleMapsApiTests : BaseTest
{
    private GoogleMapsApi _googleMapsApi;

    [SetUp]
    public async Task SetUp()
    {
        _googleMapsApi = await _testContext.GetApiAsync(apiContext => new GoogleMapsApi(apiContext));
    }

    [Test, Description("Test API with invalid KEY")]
    public async Task TestGoogleMapsDirectionsInvalidKey()
    {
        string apiKey = "api_key";
        string origin = "New York, NY";
        string destination = "Los Angeles, CA";
        var response = await _googleMapsApi.GetDirectionsAsync(origin, destination, apiKey);

        Assert.That(response.HttpStatus.Code, Is.EqualTo(200), "Status code is " + response.HttpStatus.Code);
    }
}
