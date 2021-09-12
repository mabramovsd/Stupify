using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
using Stupify.Services;

namespace Stupify.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ArtistController : Controller
    {
        private readonly IRepository<Artist> artistService;

        public ArtistController(IRepository<Artist> artistService)
        {
            this.artistService = artistService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Artists = artistService.GetList();
            return View();
        }

        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            var Artist = artistService.Get(id);
            ViewBag.Artist = Artist;
            ViewBag.Songs = Artist.Songs;
            return View();
        }
    }
}