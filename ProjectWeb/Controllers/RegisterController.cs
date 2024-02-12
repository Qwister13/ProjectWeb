using Microsoft.AspNetCore.Mvc;
using ProjectWeb.BL;
using ProjectWeb.BL.Auth;
using ProjectWeb.DAL;
using ProjectWeb.DAL.Models;
using ProjectWeb.Middleware;
using ProjectWeb.ViewMapper;
using ProjectWeb.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController : Controller
    {
        private readonly IAuth authBl;
        public RegisterController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                var IsValidEmailDomain = authBl.IsValidEmailDomain(model.Email!, new string[] { "yandex.ru", "gmail.com", "inbox" });
                if(!await IsValidEmailDomain)
                {
                    ModelState.AddModelError("Email", "Допустимы только email-адреса с доменами @yandex.ru, @gmail.com, @inbox");
                    return View("Index", model);
                }
            } 

            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateEmailException)
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже зарегистрирован");
                }
            }
            return View("Index", model);
        }

    }
}
