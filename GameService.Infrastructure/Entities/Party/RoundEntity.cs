using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Infrastructure.Entities.Party
{
    public class RoundEntity
    {
        public Guid Id { get; set; }
        public bool IsTeamRound { get; set; }
        public int Order { get; set; }

        public Guid? PartyId { get; set; }
        public PartyEntity? Party { get; set; }
        public Guid? GameId { get; set; }
        public GameEntity? Game { get; set; }
        public Guid? TeamOneId { get; set; }
        public TeamEntity? TeamOne { get; set; }
        public Guid? TeamTwoId { get; set; }
        public TeamEntity? TeamTwo { get; set; }
        public Guid? GageId { get; set; }
        public GageEntity? Gage { get; set; }
        public Guid? WinningTeamId { get; set; }
        public TeamEntity? WinningTeam { get; set; }
    }
}
