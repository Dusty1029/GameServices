using Game.Dto.Games;

namespace GameInterface.Extensions.Models.States
{
    public class SearchGameState
    {
        public SearchGameDto SearchGame { get; set; } = new();
        public bool BySerie { get; set; } = false;
        public string SearchSerie { get; set; } = string.Empty;
    }
}
