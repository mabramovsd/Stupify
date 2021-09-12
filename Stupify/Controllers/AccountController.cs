using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stupify.Services;

namespace Stupify.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserService userService;

        public AccountController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet("Register")]
        public ActionResult Register()
        {
            return View();
        }
    }
}