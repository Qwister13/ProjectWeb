namespace ProjectWeb.BL.General
{
    public interface IWebCookie
    {
        public void AddSecure(string cookieName, string value);
        void Add(string cookieName, string value);
        void Delete(string cookieName);
        string? Get(string cookieName);
    }
}