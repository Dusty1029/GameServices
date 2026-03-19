using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Entities.Party
{
    public class PartyEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsFinish { get; set; }
        public int ActualRound {  get; set; }

        public List<TeamEntity>? Teams { get; set; }
        public List<RoundEntity>? Rounds { get; set; }
    }
}
