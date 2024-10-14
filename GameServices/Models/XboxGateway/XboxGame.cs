namespace GameService.API.Models.XboxGateway
{
    public class XboxGame
    {
        public string titleId {  get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public List<string> devices { get; set; } = [];
    }
}
