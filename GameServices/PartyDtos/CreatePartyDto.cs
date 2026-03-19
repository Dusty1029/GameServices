namespace GameService.API.PartyDtos
{
    public class CreatePartyDto
    {
        public string Name { get; set; }
        public int NumberOfTeam {  get; set; }
        public List<Guid> PlayerIds { get; set; }
        public List<Guid> GameIds { get; set; }
        public List<Guid> GageIds { get; set; }

    }
}
