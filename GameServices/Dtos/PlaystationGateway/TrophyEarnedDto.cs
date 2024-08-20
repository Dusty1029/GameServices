namespace GameServices.API.Dtos.PlaystationGateway
{
    public class TrophyEarnedDto
    {
        public int trophyId { get; set; }
        public bool earned { get; set; }
        public decimal trophyEarnedRate { get; set; }
    }
}
