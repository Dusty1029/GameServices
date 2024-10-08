﻿using GameService.API.Models.Options;

namespace GameService.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<SteamOptions>(builder.Configuration.GetSection(SteamOptions.SectionName));
            builder.Services.Configure<PlaystationOption>(builder.Configuration.GetSection(PlaystationOption.SectionName));
        }
    }
}
