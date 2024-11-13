using Game.Dto.Series;

namespace GameInterface.Extensions.Models
{
    public static class SerieExtension
    {
        public static CreateSerieDto ToCreate(this SerieDto serieDto) => new()
        {
            Serie = serieDto.Serie,
            Games = serieDto.Games
        };
    }
}
