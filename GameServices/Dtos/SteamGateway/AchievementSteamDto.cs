﻿namespace GameServices.API.Dtos.SteamGateway
{
    public class AchievementSteamDto
    {
        public string apiname { get; set; } = string.Empty;
        public int achieved { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
    }
}
