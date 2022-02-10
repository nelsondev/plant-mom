using Newtonsoft.Json;

namespace Plant.Mom.Api.Transfer;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class WebResult
{
    public WebResult(object? data = null)
    {
        Data = data;
    }

    public object? Data { get; set; }
}
