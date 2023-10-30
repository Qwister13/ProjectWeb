namespace ProjectWeb.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            return httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstanst.AUTH_SESSION_PARAM_NAME) != null;          
        }
    }
}
