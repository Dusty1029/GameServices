using Game.Dto.Enums;
using MudBlazor;

namespace GameInterface.Extensions.Models.Enums
{
    public static class GameDetailStatusEnumExtensions
    {
        public static Color ToColor(this GameDetailStatusEnumDto status) => status switch
        {
            GameDetailStatusEnumDto.NotStarted => Color.Default,
            GameDetailStatusEnumDto.Started => Color.Primary,
            GameDetailStatusEnumDto.Finished => Color.Success,
            GameDetailStatusEnumDto.TotalyFinished => Color.Error,
            GameDetailStatusEnumDto.ToBuy => Color.Warning,
            _ => throw new NotImplementedException($"The status [{status} is not implemented.]"),
        };
    }
}
