using Microsoft.AspNetCore.Mvc;
using Stupify.Services;

namespace Stupify.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class MusicController : Controller
    {
        private readonly SongService songService;

        public MusicController(SongService songService)
        {
            this.songService = songService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Songs = songService.GetList();
            return View();
        }
    }
}