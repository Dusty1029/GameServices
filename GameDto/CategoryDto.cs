﻿namespace Game.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString() => Name;
    }
}
