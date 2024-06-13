using Microsoft.Playwright;
using Newtonsoft.Json;
using System.Net;

public class BaseApiResponse<T>
{
    public HttpStatus HttpStatus { get; set; }
    public T? Data { get; set; }

    public BaseApiResponse(IAPIResponse response)
    {
        HttpStatus = new HttpStatus(response.StatusText, (int)response.Status);
        Data = Deserialize<T>(response.TextAsync().Result);
    }

    protected U? Deserialize<U>(string json)
    {
        return JsonConvert.DeserializeObject<U>(json);
    }
}
