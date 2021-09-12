using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
using Stupify.Services;
using System.Linq;

namespace Stupify.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class UserLikeController : Controller
    {
        private readonly IRepository<UserLike> likeService;
        private readonly IRepository<User> userService;

        public UserLikeController(IRepository<UserLike> likeService, IRepository<User> userService)
        {
            this.likeService = likeService;
            this.userService = userService;
        }

        [HttpPost("Like/{songId}")]
        public IActionResult Like(int songId)
        {
            var user = userService.GetList().FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null)
            {
                return BadRequest();
            }
            if (likeService.GetList().Count(x => x.UserId == user.Id && x.SongId == songId) > 0)
            {
                return BadRequest();
            }

            var like = new UserLike { UserId = user.Id, SongId = songId };
            likeService.Create(like);
            return Ok();
        }

        [HttpPost("DisLike/{songId}")]
        public IActionResult DisLike(int songId)
        {
            var user = userService.GetList().FirstOrDefault(x => x.Login == User.Identity.Name);
            if (user == null)
            {
                return BadRequest();
            }

            var like = likeService.GetList().FirstOrDefault(x => x.UserId == user.Id && x.SongId == songId);
            if (like == null)
            {
                return BadRequest();
            }

            likeService.Delete(like.Id);
            return Ok();
        }
    }
}