namespace Gallery.Models.Entities
{
	public class Album
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Description { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// ���������� ����������
		public ICollection<Image> Images { get; set; } = new List<Image>();
	}
}
