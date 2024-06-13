public class HttpStatus
{
    public string? Reason { get; set; }
    public int Code { get; set; }

    public HttpStatus() { }

    public HttpStatus(string reason, int code)
    {
        Reason = reason;
        Code = code;
    }
}
