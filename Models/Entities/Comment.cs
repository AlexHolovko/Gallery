namespace Gallery.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ��'���� � �����������
        public int ImageId { get; set; }
        public Image? Image { get; set; }

        // (�����������) ��'���� � ������������
        public string? AuthorName { get; set; }
    }
}
