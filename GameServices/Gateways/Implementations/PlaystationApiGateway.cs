using CommonV2.Infrastructure.Services.Interfaces;
using GameServices.API.Dtos.PlaystationGateway;
using GameServices.API.Gateways.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using UnauthorizedAccessException = CommonV2.Models.Exceptions.UnauthorizedAccessException;

namespace GameServices.API.Gateways.Implementations
{
    public class PlaystationApiGateway : IPlaystationApiGateway
    {
        private readonly CancellationToken _cancellationToken;
        private readonly HttpClient _httpClient;
        public PlaystationApiGateway(ICancellationTokenService cancellationTokenService,
            HttpClient httpClient) 
        {
            _cancellationToken = cancellationTokenService.CancellationToken;
            _httpClient = httpClient;
        }
        public async Task<string?> GetAuthenticationToken(string npsso)
        {
            var builder = new UriBuilder("https://ca.account.sony.com/api/authz/v3/oauth/authorize");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_type"] = "offline";
            query["client_id"] = "09515159-7237-4370-9b40-3806e67c0891";
            query["response_type"] = "code";
            query["scope"] = "psn:mobile.v2.core psn:clientapp";
            query["redirect_uri"] = "com.scee.psxandroid.scecompcall://redirect";
            builder.Query = query.ToString();

            try
            {

                _httpClient.DefaultRequestHeaders.Add("Cookie", $"npsso={npsso}");
                var response = await _httpClient.GetAsync(builder.ToString());
                var location = response.Headers.Location;
                if (location != null && location.Query.Contains("?code=v3"))
                {
                    var responseQuery = HttpUtility.ParseQueryString(location.Query);
                    var code = responseQuery?["code"];

                    return code is null ? null : await GetToken(code);
                }
                else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<GamePlaystationDto>?> GetPlaystationGames(string token)
        {
            var builder = new UriBuilder("https://m.np.playstation.com/api/trophy/v1/users/me/trophyTitles");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["limit"] = "800";
            builder.Query = query.ToString();

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {token}");
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ResponseGetGamesPlaystationDto>(builder.ToString(), _cancellationToken);
                return response?.trophyTitles;
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("The token is expired.");

                throw e;
            }
        }

        private async Task<string?> GetToken(string code)
        {
            var body = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "com.scee.psxandroid.scecompcall://redirect"),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("token_format", "jwt")
            });

            string url = "https://ca.account.sony.com/api/authz/v3/oauth/token";
            try
            {
                
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic MDk1MTUxNTktNzIzNy00MzcwLTliNDAtMzgwNmU2N2MwODkxOnVjUGprYTV0bnRCMktxc1A=");
                var response = await _httpClient.PostAsync(url, body);
                var content = await response.Content.ReadAsStringAsync();

                // Suppose the content is in JSON format
                dynamic? json = JsonConvert.DeserializeObject(content);
                if (json is not null)
                {
                    return json.access_token;
                }
                else { return null; }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
