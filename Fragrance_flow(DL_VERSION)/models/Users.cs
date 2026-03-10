namespace Fragrance_flow_DL_VERSION_.models
{
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string PasswordHash { get; set; }
        public string salt { get; set; }
        public int isAdmin { get; set; } = 0;
        public int isBanned { get; set; } = 0;

        public string FullUserInfo => $"{id} - {username} - {email} - (ISBANNED) : {isBanned} - (ISADMIN) : {isAdmin}";

    }
}
