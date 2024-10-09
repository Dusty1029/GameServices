using System.ComponentModel;

namespace Game.Dto.Enums
{
    public enum GameDetailStatusEnumDto
    {
        [Description("Pas commencé")]
        NotStarted = 0,
        [Description("Commencé")]
        Started = 1,
        [Description("Fini")]
        Finished = 2,
        [Description("Fini à 100%")]
        TotalyFinished = 3
    }
}
