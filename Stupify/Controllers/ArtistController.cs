using Microsoft.AspNetCore.Mvc;
using Stupify.Services;

namespace Stupify.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ArtistController : Controller
    {
        private readonly ArtistService artistService;

        public ArtistController(ArtistService artistService)
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