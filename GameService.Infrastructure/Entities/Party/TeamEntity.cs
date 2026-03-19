using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Entities.Party
{
    public class TeamEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PlayerEntity>? Players { get; set; }
        public Guid? PartyId { get; set; }
        public PartyEntity? Party { get; set; }
    }
}
