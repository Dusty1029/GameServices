﻿using Game.Dto.Enums;
using Game.Dto.Series;

namespace Game.Dto.Games
{
    public class SearchGameDto
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public string Name { get; set; } = string.Empty;
        public SimpleSerieDto? Serie { get; set; }
        public PlatformDto? Platform { get; set; }
        public GameDetailStatusEnumDto? GameDetailStatus { get; set; }
        public List<CategoryDto>? Categories { get; set; }
    }
}
