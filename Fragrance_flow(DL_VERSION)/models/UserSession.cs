namespace Fragrance_flow_DL_VERSION_.models
{
    public class UserSession
    {

        public Guid SessionId { get; } = Guid.NewGuid();
        public int UserId { get; init; }
        public string Username { get; init; }
        public int isBanned { get; set; } = 0;
        public int isAdmin { get; set; } = 0;

        public double? Temperature { get; set; }
        public string? Location { get; set; }


        public bool IsFullyLoaded => UserId > 0 && Temperature.HasValue && isBanned == 0;

        public UserSession(int userId, string username)
        {
            UserId = userId;
            Username = username;

        }
    }
}
