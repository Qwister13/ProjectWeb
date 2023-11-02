namespace ProjectWeb.BL.Auth
{
    public interface ICurrentUser
    {
        Task <bool> IsLoggedIn();
    }
}
