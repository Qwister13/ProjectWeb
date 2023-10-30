using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]

        public string? Password { get; set; }

        public bool? RememberMe { get; set; }
    }
}
