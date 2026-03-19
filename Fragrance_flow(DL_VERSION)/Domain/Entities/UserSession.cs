namespace Fragrance_flow_DL_VERSION_.Domain.Entities
{
    public class UserSession
    {
        // Have not been using this model correctly much 
        // I know its a bad idea to use the users class because someone might get the hashes and salts
        // I plan on using this class properly once the wpf frontend is all good
        public Guid SessionId { get; } = Guid.NewGuid();
        public int UserId { get; init; }
        public string Username { get; init; }
        public int isBanned { get; set; } = 0;
        public int isAdmin { get; set; } = 0;

        public double? Temperature { get; set; }
        public string? Location { get; set; }
        public string token { get; set; }

        public bool IsFullyLoaded => UserId > 0 && Temperature.HasValue && isBanned == 0;


        public UserSession(int userId, string username)
        {
            UserId = userId;
            Username = username;

        }
    }
}
