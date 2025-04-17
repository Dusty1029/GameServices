using GameService.API.Gateways.Implementations;
using GameService.API.Gateways.Interfaces;
using GameService.API.Models.Options;

namespace GameService.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            ConfigureOptions(builder);
            AddGateways(builder);
        }
        private static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<SteamOptions>(builder.Configuration.GetSection(SteamOptions.SectionName));
            builder.Services.Configure<PlaystationOptions>(builder.Configuration.GetSection(PlaystationOptions.SectionName));
            builder.Services.Configure<XboxOptions>(builder.Configuration.GetSection(XboxOptions.SectionName));            
        }

        private static void AddGateways(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<IXboxApiGateway, XboxApiGateway>(client =>
            {
                var xboxOptions = builder.Configuration.GetSection(XboxOptions.SectionName).Get<XboxOptions>();
                client.DefaultRequestHeaders.Add("X-Authorization", xboxOptions!.ApiKey);
                client.DefaultRequestHeaders.Add("Accept-Language", "fr-FR");
                client.BaseAddress = new Uri(xboxOptions!.Url);
            });

            builder.Services.AddHttpClient<ISteamApiGateway, SteamApiGateway>(client =>
            {
                var steamOptions = builder.Configuration.GetSection(SteamOptions.SectionName).Get<SteamOptions>();
                client.BaseAddress = new Uri(steamOptions!.Url);
            });

            var playstationOptions = builder.Configuration.GetSection(PlaystationOptions.SectionName).Get<PlaystationOptions>();
            builder.Services.AddHttpClient<IPlaystationApiGateway, PlaystationApiGateway>(client =>
            {
                client.BaseAddress = new Uri(playstationOptions!.ServiceUrl);
            });
            builder.Services.AddHttpClient<IPlaystationTokenGateway, PlaystationTokenGateway>(client =>
            {
                client.BaseAddress = new Uri(playstationOptions!.TokenUrl);
            });

            builder.Services.AddHttpClient<IHowLongToBeatGateway, HowLongToBeatGateway>(client =>
            {
                client.BaseAddress = new Uri("https://howlongtobeat.com/api/user/undefined/collections/id/72947/");
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36");
            });
        }
    }
}
