namespace GameService.API.Models.PlaystationGateway
{
    public class TrophyEarned
    {
        public int trophyId { get; set; }
        public bool earned { get; set; }
        public decimal trophyEarnedRate { get; set; }
    }
}
