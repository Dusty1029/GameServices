using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Entities.Rallye
{
    public class SpecialTimeEntity
    {
        public Guid Id { get; set; }
        public TimeSpan Time { get; set; }

        public Guid SpecialId { get; set; }
        public Guid PlayerId { get; set; }
        public SpecialEntity? Special { get; set; }
        public PlayerEntity? Player { get; set; }
    }
}
