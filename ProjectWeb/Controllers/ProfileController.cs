using Microsoft.AspNetCore.Mvc;
using ProjectWeb.Middleware;
using ProjectWeb.ViewModels;
using System.Security.Cryptography;
using static ProjectWeb.Middleware.SiteNotAuthorizeAttribute;

namespace ProjectWeb.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }

        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            //if(ModelState.IsValid())
            string filename = "";
            var imageData = Request.Form.Files[0];
            if (imageData != null && imageData.Length > 0)
            {
                MD5 md5hash = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName); //Create byte and hash byte, byteArray = FileName
                byte[] hashBytes = md5hash.ComputeHash(inputBytes);    //Подсчет hash из начального inputBytes                    

                string hash = Convert.ToHexString(hashBytes); //Конвертируем hash в строку

                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" + //Берем из hash первые два символа для первой папки
                        hash.Substring(0, 4);                                //берем следующие 4 символа для следующей папки
                if (!Directory.Exists(dir)) //Проверка на наличие папки
                    Directory.CreateDirectory(dir); //Если папка ещё не создана - создаем 

                filename = dir + "/" + imageData.FileName; //Подсчитываем имя файла (можно добавить уникальный ID после второго + " ")
                using (var stream = System.IO.File.Create(filename)) //Создаем стример с помощью которого можно скопировать файлы
                    await imageData.CopyToAsync(stream); //Копируем с помощью стрима файл
            }

            return View("Index", new ProfileViewModel());
        }
    }
}
