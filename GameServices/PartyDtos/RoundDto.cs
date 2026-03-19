namespace GameService.API.PartyDtos
{
    public class RoundDto
    {
        public bool IsTeamRound { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string GageName { get; set; } = string.Empty;
        public TeamDto? TeamOne { get; set; }
        public TeamDto? TeamTwo { get; set; }
    }
}
