using CommonV2.Infrastructure.Services.Interfaces;
using GameService.API.Gateways.Interfaces;
using Newtonsoft.Json;
using System.Web;

namespace GameService.API.Gateways.Implementations
{
    public class PlaystationTokenGateway(ICancellationTokenService cancellationTokenService,
        HttpClient httpClient) : IPlaystationTokenGateway
    {
        private readonly CancellationToken _cancellationToken = cancellationTokenService.CancellationToken;

        public async Task<string?> GetAuthenticationToken(string npsso)
        {
            var builder = new UriBuilder($"{httpClient.BaseAddress}authorize");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_type"] = "offline";
            query["client_id"] = "09515159-7237-4370-9b40-3806e67c0891";
            query["response_type"] = "code";
            query["scope"] = "psn:mobile.v2.core psn:clientapp";
            query["redirect_uri"] = "com.scee.psxandroid.scecompcall://redirect";
            builder.Query = query.ToString();

            try
            {

                httpClient.DefaultRequestHeaders.Add("Cookie", $"npsso={npsso}");
                var response = await httpClient.GetAsync(builder.ToString(), _cancellationToken);
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

        private async Task<string?> GetToken(string code)
        {
            var body = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "com.scee.psxandroid.scecompcall://redirect"),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("token_format", "jwt")
            ]);

            string url = $"{httpClient.BaseAddress}token";
            try
            {

                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic MDk1MTUxNTktNzIzNy00MzcwLTliNDAtMzgwNmU2N2MwODkxOnVjUGprYTV0bnRCMktxc1A=");
                var response = await httpClient.PostAsync(url, body, _cancellationToken);
                var content = await response.Content.ReadAsStringAsync(_cancellationToken);

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
