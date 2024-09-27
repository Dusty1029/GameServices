﻿namespace Game.Dto
{
    public class SimpleGameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public override string ToString() => Name;
    }
}
