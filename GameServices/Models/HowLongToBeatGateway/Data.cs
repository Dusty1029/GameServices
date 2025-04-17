using System.Text.Json.Serialization;

namespace GameService.API.Models.HowLongToBeatGateway
{
    public class Data
    {
        [JsonPropertyName("games")]
        public List<Game> Games { get; set; } = [];
    }
}
