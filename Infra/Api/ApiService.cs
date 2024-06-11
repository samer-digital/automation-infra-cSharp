using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService(string baseURL)
    {
        _client = new HttpClient { BaseAddress = new System.Uri(baseURL) };
    }

    public async Task<HttpResponseMessage> GetAsync(string endpoint, object? parameters = null)
    {
        var uri = parameters == null ? endpoint : QueryHelpers.AddQueryString(endpoint, parameters.ToDictionary()!);
        return await _client.GetAsync(uri);
    }

    public async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
    {
        return await _client.PostAsJsonAsync(endpoint, data);
    }

    public async Task<HttpResponseMessage> PutAsync(string endpoint, object data)
    {
        return await _client.PutAsJsonAsync(endpoint, data);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await _client.DeleteAsync(endpoint);
    }
}