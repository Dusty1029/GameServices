﻿
using Game.Dto.Enums;

namespace Game.Dto
{
    public class PlatformDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PlatformEnumDto? PlatformEnum { get; set; }
        public override string ToString() => Name;
    }
}
