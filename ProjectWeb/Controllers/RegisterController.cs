using Microsoft.AspNetCore.Mvc;
using ProjectWeb.BL.Auth;
using ProjectWeb.DAL;
using ProjectWeb.DAL.Models;
using ProjectWeb.ViewMapper;
using ProjectWeb.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IAuthBL authBl;

        public RegisterController(IAuthBL authBl)
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
            if (ModelState.IsValid)
            {
                var errorModel = await authBl.ValidateEmail(model.Email ?? "");
                if (errorModel != null)
                {
                    ModelState.TryAddModelError("Email", errorModel.ErrorMessage!);
                }

            }
            
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
                await authBl.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                return Redirect("/");
            }
            return View("Index", model);
        }

    }
}
