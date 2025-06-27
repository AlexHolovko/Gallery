using Gallery.Data;
using Gallery.Models.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Gallery.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int imageId, string content)
        {
            var comment = new Comment
            {
                ImageId = imageId,
                Text = content,
                CreatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Gallery");
        }
    }
}
