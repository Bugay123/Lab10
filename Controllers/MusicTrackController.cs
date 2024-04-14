using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lab10.Models;

namespace Lab10.Controllers
{
    public class MusicTrackController : Controller
    {
        private readonly MusicDbContext _context;

        public MusicTrackController(MusicDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tracks = _context.MusicTracks.ToList();
            return View(tracks);
        }
    }
}
