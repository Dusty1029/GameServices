using Game.Dto;
using GameInterface.Models;

namespace GameInterface.Services.Interfaces
{
    public interface ISerieService
    {
        Task<ApiResult<Guid>> CreateSerie(CancellationToken cancellationToken, CreateSerieDto createSerie);
        Task<ApiResult> DeleteSerie(CancellationToken cancellationToken, Guid id);
        Task<ApiResult<List<SimpleSerieDto>>> GetAllSeries(CancellationToken cancellationToken);
        Task<ApiResult<SerieDto>> GetSerieById(CancellationToken cancellationToken, Guid id);
        Task<ApiResult> UpdateSerie(CancellationToken cancellationToken, Guid id, CreateSerieDto createSerie);
    }
}