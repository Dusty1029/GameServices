using CommonV2.Models.Exceptions;
using Game.Dto;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities;
using GameService.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameService.API.BusinessLogics.Implementations
{
    public class SerieBL(ISerieRepository serieRepository) : ISerieBL
    {
        public async Task<Guid> CreateSerie(CreateSerieDto createSerie)
        {
            if (createSerie.ParentId.HasValue)
            {
                var parentExist = await serieRepository.Exists(s => s.Id == createSerie.ParentId);
                if (!parentExist)
                    throw new NotFoundException($"Serie with id [{createSerie.ParentId.Value}] was not found.");
            }
            
            var serie = createSerie.ToEntity();
            await serieRepository.InsertAndSave(serie);

            return serie.Id;
        }

        public Task<List<SimpleSerieDto>> GetAllSeries() => serieRepository.GetAllSelect(f => f.Select(s => s.ToSimpleDto()), orderBy: f => f.OrderBy(s => s.Name));

        public async Task<SerieDto> GetSerieById(Guid id)
        {
            var serie = await serieRepository.Find(s => s.Id == id, f => f.Include(s => s.ParentSerie).Include(s => s.ChildrenSeries).Include(s => s.Games))
                ?? throw new NotFoundException($"Serie with id [{id}] was not found.");

            return serie.ToDto();
        }
    } 
}
