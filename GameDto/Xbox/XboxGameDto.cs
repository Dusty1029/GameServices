﻿using Game.Dto.Enums;

namespace Game.Dto.Xbox
{
    public class XboxGameDto
    {
        public string XboxId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public PlatformEnumDto PlatformEnum { get; set; }
    }
}
