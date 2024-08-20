namespace GameServices.API.Dtos.PlaystationGateway
{
    public class ResponseGetTrophiesByGameDto
    {
        public List<TrophyDto> trophies { get; set; } = new List<TrophyDto>();
    }
}
