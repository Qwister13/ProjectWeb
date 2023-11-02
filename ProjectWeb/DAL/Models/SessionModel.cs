namespace ProjectWeb.DAL.Models
{
    public class SessionModel
    {
        public Guid DbSessionId { get; set; }

        public string? SessionContent{ get; set; }

        public DateTime Created { get; set; }

        public DateTime LastAccessed { get; set; }

        public int? UserId { get; set; }
    }
}
