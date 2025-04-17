using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameService.API.Models.HowLongToBeatGateway
{
    public class Root
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
