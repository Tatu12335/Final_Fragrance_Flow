namespace Fragrance_flow_DL_VERSION_.interfaces
{
    public interface IPasswordhasher
    {
        public string HashPassword(string password, out byte[] salt);
        public bool VerifyPassword(string password, string hash, byte[] salt);
    }
}
