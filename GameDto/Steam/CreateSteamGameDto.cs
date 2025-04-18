﻿using Game.Dto.Series;

namespace Game.Dto.Steam
{
    public class CreateSteamGameDto
    {
        public Guid? GameId { get; set; }
        public SimpleSerieDto? Serie { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public required SteamGameDto SteamGame { get; set; }
    }
}
