using Game.Dto;
using GameService.Infrastructure.Entities;

namespace GameService.API.Extensions.Entities
{
    public static class SerieExtensions
    {
        public static SerieDto ToDto(this SerieEntity serieEntity) => new()
        {
            Id = serieEntity.Id,
            Serie = serieEntity.Name,
            ChildSeries = serieEntity.ChildrenSeries?.Select(x => x.ToSimpleDto()).ToList() ?? [],
            ParentSerie = serieEntity.ParentSerie?.ToSimpleDto(),
            Games = serieEntity.Games!.Select(g => g.ToSimpleDto()).ToList()
        };

        public static SerieDto ToDto(this SerieEntity serieEntity, List<GameEntity> gameEntities) => new()
        {
            Id = serieEntity.Id,
            Serie = serieEntity.Name,
            ChildSeries = serieEntity.ChildrenSeries?.Select(x => x.ToSimpleDto()).ToList() ?? [],
            ParentSerie = serieEntity.ParentSerie?.ToSimpleDto(),
            Games = gameEntities.Select(g => g.ToSimpleDto()).ToList()
        };

        public static SimpleSerieDto ToSimpleDto(this SerieEntity serieEntity) => new()
        {
            Id = serieEntity.Id,
            Serie = serieEntity.Name
        };

        public static SerieEntity ToEntity(this CreateSerieDto createSerieDto, SerieEntity? serieEntity = null)
        {
            serieEntity ??= new();
            serieEntity.Name = createSerieDto.Serie;
            serieEntity.ParentSerieId = createSerieDto.ParentId;
            serieEntity.Games?.ForEach(g => g.PlayOrder = createSerieDto.Games!.First(gameDto => gameDto.Id == g.Id).Order);

            return serieEntity;
        }
    }
}
