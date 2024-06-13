using Microsoft.Playwright;
using Newtonsoft.Json;

public class ApiExecutor
{
    private readonly IAPIRequestContext _apiContext;

    public ApiExecutor(IAPIRequestContext apiContext)
    {
        _apiContext = apiContext;
    }

    private async Task<IAPIResponse> SendAsync(string method, string url, Dictionary<string, string>? headers = null, string? data = null)
    {
        var requestOptions = new APIRequestContextOptions
        {
            Method = method,
            Headers = headers,
            Data = data
        };

        return await _apiContext.FetchAsync(url, requestOptions);
    }

    public async Task<BaseApiResponse<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
    {
        var response = await SendAsync("GET", url, headers);
        return new BaseApiResponse<T>(response);
    }

    public async Task<BaseApiResponse<T>> PostAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
    {
        var json = JsonConvert.SerializeObject(data);
        var response = await SendAsync("POST", url, headers, json);
        return new BaseApiResponse<T>(response);
    }

    public async Task<BaseApiResponse<T>> PutAsync<T>(string url, object data, Dictionary<string, string>? headers = null)
    {
        var json = JsonConvert.SerializeObject(data);
        var response = await SendAsync("PUT", url, headers, json);
        return new BaseApiResponse<T>(response);
    }

    public async Task<BaseApiResponse<T>> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null)
    {
        var response = await SendAsync("DELETE", url, headers);
        return new BaseApiResponse<T>(response);
    }
}
