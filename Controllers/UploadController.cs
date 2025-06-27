using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gallery.Data;
using Gallery.Models.Entities;

namespace Gallery.Controllers
{
    public class UploadController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public UploadController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: /Upload/Upload
        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            ViewBag.Albums = await _context.Albums.ToListAsync();
            return View();
        }

        // POST: /Upload/Upload
        [HttpPost]
        public async Task<IActionResult> Upload(string title, IFormFile file, int? albumId, string? newAlbumName)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Файл не выбран.");
            }

            Album? album = null;

            if (!string.IsNullOrWhiteSpace(newAlbumName))
            {
                album = new Album
                {
                    Name = newAlbumName,
                    Description = "",
                    CreatedAt = DateTime.UtcNow
                };
                _context.Albums.Add(album);
                await _context.SaveChangesAsync();
            }
            else if (albumId.HasValue)
            {
                album = await _context.Albums.FindAsync(albumId.Value);
                if (album == null)
                {
                    ModelState.AddModelError("albumId", "Выбранный альбом не существует.");
                }
            }
            else
            {
                ModelState.AddModelError("albumId", "Не выбран альбом и не введено название нового альбома.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Albums = await _context.Albums.ToListAsync();
                return View();
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new Image
            {
                Title = title,
                FilePath = uniqueFileName,
                UploadedAt = DateTime.UtcNow,
                AlbumId = album.Id
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Albums");
        }
    }
}
