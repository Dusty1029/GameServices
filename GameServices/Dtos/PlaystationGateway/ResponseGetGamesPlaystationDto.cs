namespace GameServices.API.Dtos.PlaystationGateway
{
    public class ResponseGetGamesPlaystationDto
    {
        public List<GamePlaystationDto> trophyTitles { get; set; } = new();
    }
}
