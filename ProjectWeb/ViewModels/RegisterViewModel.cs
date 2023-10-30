using ProjectWeb.BL.Auth;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.ViewModels
{
	public class RegisterViewModel : IValidatableObject 
	{
        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress(ErrorMessage = "Не правильный Email")]
        public string? Email { get; set; }
         
        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
                ErrorMessage = "Пароль слишком простой! Не менее 10 символов и содержать: " +
                                "A-Z, a-z, 0-9, !@#$%^&*-")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string[] easyPassword = { "qwerty", "123456" };
            if (Password == easyPassword.ToString())
            {
                yield return new ValidationResult("Пароль слишком легкий!", new[] { "Password" });
            }
        }

    }
}

