using Microsoft.AspNetCore.Identity;
using ProjectWeb.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);
        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task<ValidationResult?> ValidateEmail(string email);
        Task<bool> IsValidEmailDomain(string email, string[] domains);
    }
}
