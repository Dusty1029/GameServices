using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Entities.Rallye
{
    public class SpecialEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid RallyeId { get; set; }
        public RallyeEntity? Rallye { get; set; }
        public List<SpecialTimeEntity>? SpecialTimes { get; set; }
    }
}
