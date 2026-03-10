namespace Fragrance_flow_DL_VERSION_.models
{
    public class AdminSession
    {
        public Guid SessionId { get; } = Guid.NewGuid();
        public int UserId { get; init; }
        public string Username { get; init; }
        public int isBanned { get; set; } = 0;
        public int isAdmin { get; set; } = 0;

        public double? Temperature { get; set; }
        public string? Location { get; set; }

        public AdminSession(string username, int userId)
        {
            Username = username;
            UserId = userId;
        }
    }
}
