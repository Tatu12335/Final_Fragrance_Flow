using Fragrance_flow_DL_VERSION_.interfaces;
namespace Fragrance_flow_DL_VERSION_.classes.Fragrance_Engine
{
    public class FragranceEngine
    {
        //private readonly IWeatherService _weather;
       // private readonly ICli _cli;
        private readonly IFragranceRepo _repo;
        //private readonly IAdminServices _adminServices;
       // private readonly ISuggestion _suggestion;
        private readonly ILoggger _loggger;
        public FragranceEngine(/*IWeatherService weather, ICli cli,*/ IFragranceRepo repo,/* IAdminServices adminServices, ISuggestion suggestion,*/ ILoggger loggger)
        {
            //_weather = weather;
            //_cli = cli;
            _repo = repo;
            //_adminServices = adminServices;
            //_suggestion = suggestion;
            _loggger = loggger;
        }
        public async Task RUN()
        {

            await _repo.GetAllAsync();
            /*string username = await _cli.UserPanel();

            string password = _cli.UserPanel_PasswordAsync();

            double? temp = await _weather.UserLocationAsync();

            var session = await _repo.Login(username, password);
            if (session == null)
            {
                Console.WriteLine(" Session denied, Would you like to create a new user(y/n)");
                Console.Write(">");
                string answer = Console.ReadLine().ToLower();

                if (answer == null) return;

                switch (answer)
                {
                    case "y":
                        await _cli.CreateUserAsync();
                        break;
                    case "n":
                        return;
                    default:
                        throw new Exception("Invalid command");

                }

            }
           
            if (session == null)
            {
                return;

            }
            var bannedStatus = await _repo.GetBannedStatus(username);

            if (bannedStatus != null)
            {
                Console.WriteLine($" User : '{username}', Is banned");

                return;
            }

            var Id = await _repo.GetUserId(username);

            if (Id == null) return;

            var adminStatus = await _repo.GetAdminStatus(username);
            bool isRunning = true;
            _loggger.Log($" Engine started succefully for user '{username}'");
            if (adminStatus != null)
            {
                while (isRunning)
                {
                    var command = await _cli.LoadAdminPanel();

                    if (command == "exit" || string.IsNullOrEmpty(command))
                    {
                        isRunning = false;
                        _loggger.Log($" Admin '{username}',quit the program");
                        _loggger.Dispose();
                        continue;
                    }
                    await _cli.ExecuteAdminCommand(command);



                }

            }

            while (isRunning)
            {
                string command = _cli.OnceLoggedIn(username);

                if (command == "exit" || string.IsNullOrEmpty(command))
                {
                    isRunning = false;
                    _loggger.Log($" user '{username}',quit the program");
                    _loggger.Dispose();
                    continue;

                }

                await _cli.ExecuteCommand(command, username, Id.id);

            }*/


        }
    }
}
