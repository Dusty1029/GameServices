namespace GameServices.API.Dtos.PlaystationGateway
{
    public class ResponseGetTrophiesEarnedByGameDto
    {
        public List<TrophyEarnedDto> trophies { get; set; } = new List<TrophyEarnedDto>();
    }
}
