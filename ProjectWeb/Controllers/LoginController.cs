using Microsoft.AspNetCore.Mvc;
using ProjectWeb.BL;
using ProjectWeb.BL.Auth;
using ProjectWeb.DAL;
using ProjectWeb.DAL.Models;
using ProjectWeb.ViewModels;
using System.Text.RegularExpressions;
using ProjectWeb.Middleware;

namespace ProjectWeb.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuth authBl;
        public LoginController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel  model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.CheckEmail(model.Email ?? "");
                }
                catch (AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Email ещё не зарегистрирован");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Email или пароль неверный");
                }

            }
            return View("Index", model);
        }

        
    }
}
