using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lab10.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Controllers
{
    public class MusicTrackController(MusicDbContext context) : Controller
    {
        private readonly MusicDbContext _context = context;

        public IActionResult Index()
        {
            var tracks = _context.MusicTracks.ToList();
            return View(tracks);
        }
    }
}
