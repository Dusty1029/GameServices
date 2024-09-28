using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ISerieService
    {
        Task<ApiResult<Guid>> CreateSerie(CreateSerieDto createSerie);
        Task<ApiResult<List<SimpleSerieDto>>> GetAllSeries();
        Task<ApiResult<SerieDto>> GetSerieById(Guid id);
    }
}
