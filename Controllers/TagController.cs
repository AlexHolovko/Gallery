using Gallery.Data;
using Gallery.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Gallery.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> AddTag(int imageId, string tagName)
        {
            var image = await _context.Images.Include(i => i.Tags).FirstOrDefaultAsync(i => i.Id == imageId);
            if (image != null)
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName) ?? new Tag { Name = tagName };
                image.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Gallery");
        }
    }
}
