namespace Game.Dto.Series
{
    public class SimpleSerieDto
    {
        public Guid Id { get; set; }
        public string Serie { get; set; } = string.Empty;
        public bool CanBeDeleted { get; set; }

        public override string ToString() => Serie;
    }
}
