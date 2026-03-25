namespace Fragrance_flow_DL_VERSION_.Domain.Entities
{
    // The user model
    public class Users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string PasswordHash { get; set; }
        public string salt { get; set; }
        public int isAdmin { get; set; }
        public int isBanned { get; set; }

        // FullUserInfo because displaymemberpath in wpf
        public string FullUserInfo => $"{id} - {username} - {email} - (ISBANNED) : {isBanned} - (ISADMIN) : {isAdmin}";

    }
}
