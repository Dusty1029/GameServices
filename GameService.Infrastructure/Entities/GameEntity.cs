﻿namespace GameService.Infrastructure.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public Guid? SerieId { get; set; }
        public SerieEntity? Serie { get; set; }
        public List<CategoryEntity>? Categories { get; set;}
        public List<GameDetailEntity>? GameDetails { get; set;}
    }
}
