using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gallery.Models.Entities; 
using System;
using System.Threading.Tasks;
using Gallery.Data;

namespace Gallery.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly AppDbContext _context;

        public AlbumsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Albums/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var albums = await _context.Albums.ToListAsync();
            return View(albums);
        }

        // POST: /Albums/Create
        [HttpPost]
        public async Task<IActionResult> Create(string Name, string Description)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ModelState.AddModelError("Name", "Название альбома обязательно");
            }

            if (!ModelState.IsValid)
            {
                var albums = await _context.Albums.ToListAsync();
                return View("Index", albums);
            }

            var album = new Album
            {
                Name = Name,
                Description = Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Альбом успешно создан";

            var updatedAlbums = await _context.Albums.ToListAsync();
            return View("Index", updatedAlbums);
        }

        // GET: /Albums/Detail/{id}
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Images) // загружаем изображения альбома
                .FirstOrDefaultAsync(a => a.Id == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
    }
}
