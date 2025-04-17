using CommonV2.Infrastructure.Services.Interfaces;
using GameService.API.Gateways.Interfaces;
using GameService.API.Models.HowLongToBeatGateway;
using System.Text;
using System.Text.Json;

namespace GameService.API.Gateways.Implementations
{
    public class HowLongToBeatGateway(ICancellationTokenService cancellationTokenService,
        HttpClient httpClient): IHowLongToBeatGateway
    {
        public async Task<Models.HowLongToBeatGateway.Game?> FindHowLongToBeatGame(string game)
        {
            var body = new
            {
                listId = 72947,
                options = new
                {
                    name = new[] { game.ToLower().Trim() },
                    platform = "",
                    sortType = "custom_order",
                    sortView = "card",
                    sortOrder = 0
                },
                page = 1,
                maxResults = 50,
                randomSeed = 72947
            };
            var jsonBody = JsonSerializer.Serialize(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("", content, cancellationTokenService.CancellationToken);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Root>(json);

            return result?.Data.Games.Find(g => g.Name.ToLower().Trim() == game.ToLower().Trim());
        }
    }
}
