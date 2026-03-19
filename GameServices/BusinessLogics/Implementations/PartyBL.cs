using CommonV2.Extensions;
using GameService.API.BusinessLogics.Interfaces;
using GameService.API.Extensions.Entities.Party;
using GameService.API.PartyDtos;
using GameService.Infrastructure.Entities.Party;
using GameService.Infrastructure.Repositories.Interfaces.Party;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameService.API.BusinessLogics.Implementations
{
    public class PartyBL(IGageRepository gageRepository,
        IGameRepository gameRepository,
        IPlayerRepository playerRepository,
        IPartyRepository partyRepository) : IPartyBL
    {
        private static readonly Random random = new();
        public async Task<Guid> CreateGage(CreateGageDto createGage) =>
            (await gageRepository.InsertAndSave(createGage.ToEntity())).Id;

        public async Task<Guid> CreateGame(CreatePartyGameDto createGame) =>
            (await gameRepository.InsertAndSave(createGame.ToEntity())).Id;

        public async Task<Guid> CreateParty(CreatePartyDto createParty)
        {
            PartyEntity partyEntity = new() { Name = createParty.Name, IsFinish = false, ActualRound = 1 };
            List<PlayerEntity> players = await playerRepository.Get(p => createParty.PlayerIds.Contains(p.Id), noTracking: false);
            List<GameEntity> games = await gameRepository.Get(g => createParty.GameIds.Contains(g.Id), noTracking: false);
            List<GageEntity> gages = await gageRepository.Get(g => createParty.GageIds.Contains(g.Id), noTracking: false);
            partyEntity.Teams = GenerateTeams(createParty.NumberOfTeam, players);

            int round = 1;
            List<RoundEntity> rounds = [];

            games.Shuffle();

            foreach (GameEntity game in games)
            {
                gages.Shuffle();
                int gageIndex = 0;

                if (game.IsTeamGame)
                {
                    rounds.Add(new RoundEntity
                    {
                        GameId = game.Id,
                        GageId = gages[0].Id,
                        IsTeamRound = true,
                        Order = round++
                    });

                    continue;
                }

                List<Guid> teamIds = partyEntity.Teams.Select(t => t.Id).ToList();
                teamIds.Shuffle();

                int roundNumber = round;
                var teamPairs = new List<(Guid Team1, Guid Team2)>();

                for (int i = 0; i < teamIds.Count; i += 2)
                {
                    if (i + 1 >= teamIds.Count) break;

                    teamPairs.Add((teamIds[i], teamIds[i + 1]));
                }

                foreach (var pair in teamPairs)
                {
                    rounds.Add(new RoundEntity
                    {
                        GameId = game.Id,
                        GageId = gages[gageIndex % gages.Count].Id,
                        IsTeamRound = game.IsTeamGame,
                        Order = roundNumber++,
                        TeamOneId = pair.Team1,
                        TeamTwoId = pair.Team2
                    });

                    gageIndex++;
                }

                round = roundNumber;
            }

            partyEntity.Rounds = rounds;

            return (await partyRepository.InsertAndSave(partyEntity)).Id;
        }

        public async Task<Guid> CreatePlayer(CreatePlayerDto createPlayer) => 
            (await playerRepository.InsertAndSave(createPlayer.ToEntity())).Id;

        public async Task<List<GageDto>> GetAllGages() =>
            (await gageRepository.GetAll(orderBy: f => f.OrderBy(p => p.Name))).Select(p => p.ToDto()).ToList();

        public async Task<List<PartyGameDto>> GetAllGames() =>
            (await gameRepository.GetAll(orderBy: f => f.OrderBy(p => p.Name))).Select(p => p.ToDto()).ToList();

        public async Task<List<SimplePartyDto>> GetAllParty() =>
            (await partyRepository.GetAll(orderBy: f => f.OrderBy(p => p.Name))).Select(p => p.ToSimpleDto()).ToList();

        public async Task<List<PlayerDto>> GetAllPlayers() => 
            (await playerRepository.GetAll(orderBy: f => f.OrderBy(p => p.Name))).Select(p => p.ToDto()).ToList();

        private static List<TeamEntity> GenerateTeams(int numberOfTeam, List<PlayerEntity> players)
        {
            var teams = new List<TeamEntity>();
            var usedNames = new HashSet<string>();

            players.Shuffle();

            for (int i = 0; i < numberOfTeam; i++)
            {
                string name;
                do
                {
                    name = GenerateTeamName();
                }
                while (!usedNames.Add(name));

                teams.Add(new TeamEntity
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Players = []
                });
            }

            int index = 0;
            foreach (var player in players)
            {
                teams[index].Players!.Add(player);
                index = (index + 1) % numberOfTeam;
            }

            return teams;
        }

        private static string GenerateTeamName()
        {
            string[] prefixes = { "Les", "Team", "Squad", "Groupe", "Clan" };
            string[] nouns = { "Lynx", "Dragons", "Phénix", "Tigres", "Loups", "Requins", "Vikings", "Bandeurs" };

            return $"{prefixes[random.Next(prefixes.Length)]} {nouns[random.Next(nouns.Length)]}";
        }

        public async Task Clear()
        {
            await partyRepository.DeleteRangeByPredicateAndSave(p => true);
        }

        public async Task<PartyDto> GetPartyById(Guid partyId)
        {
            PartyEntity? party = await partyRepository.Find(
                p => p.Id == partyId,
                f => f.Include(p => p.Teams)!.ThenInclude(t => t.Players)
                      .Include(p => p.Rounds)!.ThenInclude(r => r.Game)
                      .Include(p => p.Rounds)!.ThenInclude(r => r.Gage)
                      .Include(p => p.Rounds)!.ThenInclude(r => r.TeamOne)
                      .Include(p => p.Rounds)!.ThenInclude(r => r.TeamTwo)
            );

            PartyDto partyDto = party!.ToDto();
            partyDto.Teams.ForEach(
                team => team.Points = party!.Rounds!.Count(r => r.WinningTeamId == team.Id)
            );
            partyDto.Teams = partyDto.Teams.OrderByDescending(t => t.Points).ThenBy(t => t.Name).ToList();

            return partyDto;
        }

        public async Task<PartyDto> GetNextRound(Guid partyId, Guid winningTeamId)
        {
            PartyEntity? party = await partyRepository.Find(
                p => p.Id == partyId,
                f => f.Include(p => p.Rounds),
                noTracking: false
            );

            if(!party!.IsFinish)
            {
                RoundEntity actualRound = party!.Rounds!.First(r => r.Order == party.ActualRound);

                if (party!.ActualRound == party.Rounds!.Max(r => r.Order))
                {
                    party.IsFinish = true;
                }
                else
                {
                    party.ActualRound++;
                }

                actualRound.WinningTeamId = winningTeamId;

                await partyRepository.SaveChanges();
            }
            
            return await this.GetPartyById(partyId);
        }

        public async Task<PartyDto> CancelPreviousRound(Guid partyId)
        {
            PartyEntity? party = await partyRepository.Find(
                p => p.Id == partyId,
                f => f.Include(p => p.Rounds),
                noTracking: false
            );
            if(party!.ActualRound != 1)
            {
                RoundEntity previousRound = party!.Rounds!.First(r => r.Order == (party.IsFinish ? party.ActualRound : (party.ActualRound - 1)));
                if (!party.IsFinish)
                {
                    party.ActualRound--;
                }
                party!.IsFinish = false;

                previousRound.WinningTeamId = null;

                await partyRepository.SaveChanges();
            }

            return await this.GetPartyById(partyId);
        }
    }
}
