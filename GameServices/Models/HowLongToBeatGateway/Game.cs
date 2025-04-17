using System.Text.Json.Serialization;

namespace GameService.API.Models.HowLongToBeatGateway
{
    public class Game
    {
        [JsonPropertyName("game_name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("comp_main")]
        public int MainStoryTime { get; set; }
        [JsonPropertyName("comp_plus")]
        public int MainStoryAndExtraTime { get; set; }
        [JsonPropertyName("comp_100")]
        public int FullTime { get; set; }
    }
}
