namespace GameService.API.Models.Options
{
    public class SteamOptions
    {
        public const string SectionName = "Steam";
        public string Url { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string SteamId { get; set; } = string.Empty;

    }
}
