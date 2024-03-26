using Microsoft.AspNetCore.Mvc;

namespace ProjectWeb.ViewComponents
{
    public class MainMenuComponent : ViewComponent
    {
        public MainMenuComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            bool isLoggedIn = true;
            return View("Index", isLoggedIn);
        }

    }
}
