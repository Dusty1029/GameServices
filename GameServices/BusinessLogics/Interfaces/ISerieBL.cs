
using Game.Dto;

namespace GameService.API.BusinessLogics.Interfaces
{
    public interface ISerieBL
    {
        Task<Guid> CreateSerie(CreateSerieDto createSerie);
        Task<List<SimpleSerieDto>> GetAllSeries();
        Task<SerieDto> GetSerieById(Guid id);
    }
}
