﻿namespace Game.Dto
{
    public class CreateXboxGameDto
    {
        public Guid? GameId { get; set; }
        public SimpleSerieDto? Serie { get; set; }
        public IEnumerable<CategoryDto>? Categories { get; set; }
        public required XboxGameDto XboxGame { get; set; }
    }
}