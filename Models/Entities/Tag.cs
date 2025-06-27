namespace Gallery.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // ������-��-�������� � Image
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
