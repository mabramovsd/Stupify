using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stupify.Model;
using Stupify.Services;
using Stupify.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public static string EncryptPassword(string Password)
        {
            var data = Encoding.UTF8.GetBytes(Password);
            using SHA512 shaM = new SHA512Managed();
            var hashedInputBytes = shaM.ComputeHash(data);
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userService.GetList().FirstOrDefault(x => x.Login == model.Login && x.Password == EncryptPassword(model.Password));
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Music");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {                
                if (userService.GetList().FirstOrDefault(x => x.Login == model.Login) == null)
                {
                    User user = new User { Login = model.Login, Password = EncryptPassword(model.Password), Role = "Пользователь" };
                    userService.Create(user);

                    await Authenticate(user);
                    return RedirectToAction("Index", "Music");
                }
                ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }
            return View(model);
        }
    }
}