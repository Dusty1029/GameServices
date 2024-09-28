namespace Game.Dto
{
    public class SimpleSerieDto
    {
        public Guid Id { get; set; }
        public string Serie { get; set; } = string.Empty;

        public override string ToString() => Serie;
    }
}
