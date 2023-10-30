using System.Reflection.Metadata.Ecma335;

namespace ProjectWeb.BL.Auth
{
    public interface IEncrypt
    {
        string HashPassword(string password, string salt);
    }
}
