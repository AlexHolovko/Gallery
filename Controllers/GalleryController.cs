using Gallery.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Gallery.Controllers
{
    public class GalleryController : Controller
    {
        private readonly AppDbContext _context;

        public GalleryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var images = await _context.Images
                .Include(i => i.Tags)
                .Include(i => i.Album)
                .ToListAsync();

            return View(images);
        }
    }
}
