using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
using Stupify.Services;
using System.Linq;

namespace Stupify.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class MusicController : Controller
    {
        private readonly IRepository<Song> songService;
        private readonly IRepository<User> userService;

        public MusicController(IRepository<Song> songService, IRepository<User> userService)
        {
            this.songService = songService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Songs = songService.GetList();
            
            var user = userService.GetList().FirstOrDefault(u => u.Login == User.Identity.Name);
            if (user != null)
            {
                var likedSongs = user.Likes.Select(l => l.SongId).ToList();
                ViewBag.LikedSongs = likedSongs;
            }

            return View();
        }

        [HttpGet("My")]
        public ActionResult My()
        {
            var user = userService.GetList().FirstOrDefault(u => u.Login == User.Identity.Name);
            var likedSongs = user.Likes.Select(l => l.SongId).ToList();
            var songs = songService.GetList().Where(s => likedSongs.Contains(s.Id)).ToList();
            ViewBag.LikedSongs = likedSongs;
            ViewBag.Songs = songs;
            return View("Index");
        }
    }
}