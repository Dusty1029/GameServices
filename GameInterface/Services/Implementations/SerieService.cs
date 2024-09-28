using Game.Dto;
using GameInterface.Models;
using GameInterface.Services.Interfaces;

namespace GameInterface.Services.Implementations
{
    public class SerieService(IGenericService genericService) : ISerieService
    {
        private readonly string beginPath = "serie";
        public Task<ApiResult<Guid>> CreateSerie(CreateSerieDto createSerie) => genericService.PostResult<Guid>(createSerie, beginPath);

        public Task<ApiResult<List<SimpleSerieDto>>> GetAllSeries() => genericService.GetResult<List<SimpleSerieDto>>(beginPath);

        public Task<ApiResult<SerieDto>> GetSerieById(Guid id) => genericService.GetResult<SerieDto>($"beginPath/{id}");
    }
}
