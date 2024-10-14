namespace GameService.API.Models.Options
{
    public class XboxOptions
    {
        public const string SectionName = "Xbox";
        public string Url { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
