using Microsoft.AspNetCore.Mvc;
using ProjectWeb.BL.Auth;
using ProjectWeb.DAL;
using ProjectWeb.DAL.Models;
using ProjectWeb.ViewModels;

namespace ProjectWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthBL authBl;
        public LoginController(IAuthBL authBl)
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
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginError = await authBl.ValidateEmail(model.Email!);
                if (loginError == null)
                    ModelState.AddModelError("Email", "Email ещё не зарегистрирован");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (ProjectWeb.BL.AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Email или пароль неверный");
                }

            }
            return View("Index", model);
        }

        
    }
}
