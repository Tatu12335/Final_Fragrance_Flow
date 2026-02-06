using Fragrance_flow_DL_VERSION_.models;

namespace Fragrance_flow_DL_VERSION_.interfaces
{

    public interface ICli
    {
        public Task<string> LoadAdminPanel();
        public string Feeling();
        public Task<string> UserPanel();
        public string UserPanel_PasswordAsync();
        public string OnceLoggedIn(string username);
        public void ShowPrompt();
        public Task ExecuteCommand(string command, string username, int id);
        public Fragrance AddFragrance(string username);
        public Task CreateUserAsync();
        public Task ExecuteAdminCommand(string command);
        public Task SUGGEST(double temp, int id);
    }
}
