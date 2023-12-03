using Microsoft.AspNetCore.Identity;
using ProjectWeb.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectWeb.BL.Auth
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel user);
        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task ValidateEmail(string email);
        Task CheckEmail(string email);
        Task<bool> IsValidEmailDomain(string email, string[] domains);
        Task Register(UserModel user);
    }
}
