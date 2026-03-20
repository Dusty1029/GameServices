namespace GameService.API.RallyeDtos
{
    public class CreateRallyeDto
    {
        public string Name { get; set; }
        public List<Guid> PlayerIds { get; set; }
    }
}
