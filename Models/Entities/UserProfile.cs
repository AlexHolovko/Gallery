namespace Gallery.Models.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // У майбутньому: зв'язки з зображеннями або коментарями
    }
}
