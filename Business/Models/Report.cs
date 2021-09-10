using Business.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Business.Models;

public class Report
{
    [JsonConverter(typeof(StringEnumConverter))]
    public DeliveryMethod DeliveryMethod { get; set; }
}

