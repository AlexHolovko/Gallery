namespace Gallery.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public int AlbumId { get; set; }
        public Album? Album { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}