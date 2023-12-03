using ProjectWeb.BL.Auth;

namespace ProjectWeb.BL.General
{
    public class WebCookie : IWebCookie
    {
        IHttpContextAccessor httpContextAccessor;

        public WebCookie(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void AddSecure(string cookieName, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            options.HttpOnly = true;
            options.Secure = true;
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value.ToString(), options);
        }

        public void Add(string cookieName, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Path = "/";
            httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, value.ToString(), options);
        }

        public void Delete(string cookieName)
        {
            httpContextAccessor?.HttpContext?.Response.Cookies.Delete(cookieName);
        }

        public string? Get(string cookieName)
        {
            var cookie = httpContextAccessor?.HttpContext?.Request?.Cookies.FirstOrDefault(m => m.Key == cookieName);
            if (cookie != null && cookie.Value.Value != null)
                return cookie.Value.Value;
            return null;
        }
    }
}
