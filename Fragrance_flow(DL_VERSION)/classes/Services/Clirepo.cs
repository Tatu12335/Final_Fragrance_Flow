
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using System.Text;

namespace Fragrance_flow_DL_VERSION_
{
    public class Clirepo : ICli
    {
        private readonly IFragranceRepo _repo;
        private readonly IAdminServices _adminServices;
        private readonly ISuggestion _suggestion;
        private readonly IWeatherService _weatherService;
        private readonly ILoggger _logger;
        public Clirepo(IFragranceRepo repo, IAdminServices adminServices, ISuggestion suggestion, IWeatherService weatherService, ILoggger logger)
        {
            _repo = repo;
            _adminServices = adminServices;
            _suggestion = suggestion;
            _weatherService = weatherService;
            _logger = logger;
        }

        public async Task<string> UserPanel()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-------------------------");
            Console.WriteLine(" Fragrance flow");
            Console.WriteLine("-------------------------");
            Console.WriteLine(" Sign in With a username");
            Console.Write(">");
            var username = Console.ReadLine();
            if (string.IsNullOrEmpty(username)) return null;

            return username;

        }
        public string UserPanel_PasswordAsync()
        {

            Console.WriteLine(" Enter password ");
            Console.Write(">");

            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
                {
                    password.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");

                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password.ToString();
        }
        public async Task<string> LoadAdminPanel()
        {
            _logger.Log(" Admin panel loaded.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Welcome to the admin-panel! ");
            Console.WriteLine("");
            Console.WriteLine(" Options : ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" \"view-users\" - shows all registered users in the database");
            Console.WriteLine(" \"remove --userId\" - removes given user from the users database");
            Console.WriteLine(" \"ban --userId\" - Bans a given user");
            Console.WriteLine(" \"unban --userId\" - Unbans a given user");
            Console.WriteLine(" \"promote --userId\" - Promote user to admin");
            Console.WriteLine(" \"demote --userId\" - Demote user to have normal privilages");
            Console.WriteLine(" \"exit\" - to exit the app");
            Console.ResetColor();

            Console.Write(">");
            string command = Console.ReadLine().ToLower();
            if (command == null) return null;
            return command;

        }
        public async Task SUGGEST(double temp, int id)
        {

            var fragrance = await _suggestion.ScentOfTheDay(temp, id);

            foreach (var item in fragrance)
            {
                Console.WriteLine("");
                Console.WriteLine($" [TEMPERATURE : {temp}], I suggest {item.Brand},{item.Name}");
                Console.WriteLine("");
            }
        }
        public async Task ExecuteAdminCommand(string command)
        {
            Console.Clear();
            switch (command)
            {
                // didn't use a library like CommandLineParser, because i wanted to learn to parse the input myself.
                case string s when s.StartsWith("ban"):
                    try
                    {
                        var parts = s.Split(' ');

                        if (parts.Length < 2)
                        {
                            Console.WriteLine(" Error : Give the id 'ban --id'");
                            break;
                        }

                        string idstring = parts[1].Replace("-", "");

                        if (int.TryParse(idstring, out int id))
                        {
                            await _adminServices.BanUserById(id);
                            Console.WriteLine($" User {id} banned successfully");
                            _logger.Log($" User : '{id}', Banned successfully");
                        }
                        else
                        {
                            Console.WriteLine(" ERROR : ID need's to be a number");
                            _logger.Log(" [error] : Id need's to be a number");

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" An error occured : " + ex.Message);
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    }

                    break;

                case "view-users":

                    var adminViewUsers = await _adminServices.GetAllUsers();
                    _logger.Log("Admin used 'view-users'.");
                    foreach (var user in adminViewUsers)
                    {
                        Console.WriteLine($" Username : {user.username}, ID : {user.id}, Banned-Status : {user.isBanned}, ROLE : {user.isAdmin}.");
                    }

                    break;

                case string s when s.StartsWith("unban"):
                    try
                    {
                        var parts = s.Split(' ');
                        if (parts.Length < 2)
                        {
                            Console.WriteLine(" Error : Give the id 'unban --id'");
                            break;
                        }

                        string idstring = parts[1].Replace("-", "");

                        if (int.TryParse(idstring, out int id))
                        {
                            await _adminServices.UnbanUserById(id);
                            Console.WriteLine($" User {id} Unbanned successfully");
                            _logger.Log($" User : '{id}', Unbanned successfully");
                        }
                        else
                        {
                            Console.WriteLine(" ERROR : ID needs to be a number");
                            _logger.Log(" [error] : Id need's to be a number");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" An error occured : " + ex.Message);
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    }
                    break;

                case string s when s.StartsWith("promote"):
                    try
                    {
                        var parts = s.Split(' ');
                        if (parts.Length < 2)
                        {
                            Console.WriteLine(" Error : Give the id 'promote --id'");
                            break;
                        }

                        string idstring = parts[1].Replace("-", "");

                        if (int.TryParse(idstring, out int id))
                        {
                            await _adminServices.PromoteUserById(id);
                            Console.WriteLine($" User {id} Promoted successfully");
                            _logger.Log($" User : '{id}', Promoted successfully");
                        }
                        else
                        {
                            Console.WriteLine(" ERROR : ID needs to be a number");
                            _logger.Log(" [error] : Id need's to be a number");

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" An error occured : " + ex.Message);
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    }
                    break;
                case string s when s.StartsWith("demote"):
                    try
                    {
                        var parts = s.Split(' ');
                        if (parts.Length < 2)
                        {
                            Console.WriteLine(" Error : Give the id 'demote --id'");
                            break;
                        }

                        string idstring = parts[1].Replace("-", "");

                        if (int.TryParse(idstring, out int id))
                        {
                            await _adminServices.DemoteUserById(id);
                            Console.WriteLine($" User {id} demoted successfully");
                            _logger.Log($" User : '{id}', demoted successfully");
                        }
                        else
                        {
                            Console.WriteLine(" ERROR : ID need's to be a number");
                            _logger.Log(" [error] : Id need's to be a number");

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" An error occured : " + ex.Message);
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    }
                    break;
                case string s when s.StartsWith("remove"):
                    try
                    {
                        var parts = s.Split(' ');
                        if (parts.Length < 2)
                        {
                            Console.WriteLine(" Error : Give the id 'remove --id'");
                            break;
                        }

                        string idstring = parts[1].Replace("-", "");

                        if (int.TryParse(idstring, out int id))
                        {
                            await _adminServices.RemoveUserById(id);
                            Console.WriteLine($" User {id} removed successfully");
                            _logger.Log($" Admin , removed user : '{id}'");
                        }
                        else
                        {
                            Console.WriteLine(" ERROR : ID needs to be a number");

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" An error occured : " + ex.Message);
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    }
                    break;
                default:
                    Console.WriteLine(" Invalid command");
                    _logger.Log(" Invalid command");
                    break;
            }

        }
        public string OnceLoggedIn(string username)
        {


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"------ WELCOME BACK [{username}] ------");
            ShowPrompt();
            Console.Write(">");
            string command = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrEmpty(command)) return "";
            return command;


        }
        // TO DO : Add filtering logic for listing
        public async Task ExecuteCommand(string command, string username, int id)
        {
            Console.Clear();
            switch (command)
            {
                case "add":
                    try
                    {
                        _logger.Log($" User : '{username}', used add");
                        var frag = AddFragrance(username);
                        await _repo.AddFragrance(username, frag); ;

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" Error adding fragrance: {ex.Message}");
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                        Console.ResetColor();
                        break;
                    }

                case "list":
                    try
                    {
                        _logger.Log($" User : {username} used 'list'");
                        var frags = await _repo.GetFragrancesByUserId(username, id);
                        foreach (var frag in frags)
                        {
                            Console.WriteLine($"ID : {frag.id} Name : {frag.Name}, Brand : {frag.Brand}, Category : {frag.category}, Notes : {frag.notes}, Occasion :{frag.occasion}.");
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($" Error listing fragrances : {ex.Message}");
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                        Console.ResetColor();
                        break;
                    }
                case string s when s.StartsWith("remove"):
                    try
                    {
                        var parts = s.Split(' ');
                        if (parts.Length < 2)
                        {
                            Console.WriteLine(" Error : Give the id 'remove --id'");
                            break;
                        }

                        string idstring = parts[1].Replace("-", "");

                        if (int.TryParse(idstring, out int Id))
                        {
                            await _repo.RemoveFragranceById(id, Id);
                            Console.WriteLine($" Fragrance : {Id} removed successfully");
                            _logger.Log($" Fragrance : {Id}, removed from users : '{username}' collection");
                        }
                        else
                        {
                            Console.WriteLine(" ERROR : ID need's to be a number");
                            _logger.Log($" [error] : Id need's to be a number");
                            return;

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" An error occured : " + ex.Message);
                    }
                    break;
                case "sotd":
                    try
                    {
                        double temp = Convert.ToDouble(await _weatherService.UserLocationAsync());
                        await SUGGEST(temp, id);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Error on main's scentoftheday call : " + ex.Message);
                        _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                        Console.ResetColor();
                        break;
                    }
                case "help":
                    ShowPrompt();
                    _logger.Log($" User : '{username}', used 'help'");
                    break;

                case "suggest -f":

                    var answer = Feeling();
                    var suggestion = await _suggestion.SuggestBasedOnFeeling(answer, id);
                    if (suggestion == null)
                    {
                        Console.WriteLine(" No suitable fragrance in your collection");
                        return;
                    }

                    Console.WriteLine($" Todays suggestion Is : [{suggestion.Brand}, {suggestion.Name}]");
                    _logger.Log($" User : '{username}', used 'suggest -f'.");

                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Invalid command. Type 'help' to see available commands.");
                    _logger.Log($" User : '{username}', used an invalid command");
                    break;

            }
        }
        public async Task CreateUserAsync()
        {
            Console.WriteLine(" Please enter a username for the new user:");
            Console.Write(">");
            var username = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(username))
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Username cannot be empty.");
                Console.ResetColor();
                return;
            }
            _logger.Log($" User : '{username}' Created user ");
            var userExists = await _repo.CheckIfUserExists(username);
            if (userExists != null) return;

            Console.WriteLine(" Please enter email for the new user");
            Console.Write(">");
            var email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email)) return;

            var emailVerification = await _repo.VerifyEmail(email);

            if (emailVerification != 0)
            {
                _logger.Log($" Email : '{email}', was already in use");
                Console.WriteLine($" Email : {email}, already in use ");
                return;
            }



            var password = UserPanel_PasswordAsync();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Password cannot be empty.");
                Console.ResetColor();
                return;
            }

            await _repo.CreateNewUserAsync(username, email, password);
            _logger.Log($" User created successfully with Username '{username}', Email '{email}'.");
            Console.ResetColor();



        }
        public Fragrance AddFragrance(string username)
        {
            Console.WriteLine(" Whats the name of the fragrance");
            Console.Write(">");
            var Name = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Name))
            {
                Console.WriteLine(" Name cannot be empty. Aborting fragrance addition.");
                return null;
            }

            Console.WriteLine(" Whats the brand of the fragrance");
            Console.Write(">");
            var Brand = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Brand))
            {
                Console.WriteLine(" Brand cannot be empty. Aborting fragrance addition.");
                return null;
            }

            Console.WriteLine(" List some notes (comma separated)");
            Console.Write(">");
            var Notes = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Notes))
            {
                Console.WriteLine(" Notes cannot be empty. Aborting fragrance addition.");
                return null;
            }

            Console.WriteLine(" Whats the category (gourmand,fresh,amber,etc.)");
            Console.Write(">");
            var Category = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Category))
            {
                Console.WriteLine(" Category cannot be empty. Aborting fragrance addition.");
                return null;
            }

            Console.WriteLine(" Most common weather to wear it in (cold, warm, etc.)");
            Console.Write(">");
            var Weather = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(Weather))
            {
                Console.WriteLine(" Weather cannot be empty. Aborting fragrance addition.");
                return null;
            }

            Console.WriteLine(" Most common occasion to wear it in (casual, formal, etc.)");
            Console.Write(">");
            var Occasion = Console.ReadLine()?.Trim().ToLower(); ;
            if (string.IsNullOrWhiteSpace(Occasion))
            {
                Console.WriteLine(" Occasion cannot be empty. Aborting fragrance addition.");
                return null;
            }
            Console.WriteLine($" Fragrance : {Brand}, {Name}, added successfully");
            _logger.Log($" Fragrance : {Brand}, {Name}, added successfully");
            return new Fragrance { Name = Name, Brand = Brand, category = Category, notes = Notes, occasion = Occasion, weather = Weather };
        }
        public void ShowPrompt()
        {
            // Console.Clear();
            Console.WriteLine("");
            string ArtXD = @"
                             _
                            (_)
                           |---|
                           |   |
                           |   |
                           '---'";
            Console.WriteLine(ArtXD);
            Console.WriteLine(" Please enter a command to manage your fragrance inventory.");
            Console.WriteLine(" Available commands:");
            Console.WriteLine(" add - Add a new fragrance");
            Console.WriteLine(" list - List all fragrances");
            Console.WriteLine(" 'remove --id' - Remove a fragrance by ID");
            Console.WriteLine(" TIP : Use \"suggest -f\", To get suggestion based feelings");
            Console.WriteLine(" sotd - Scent of the day");
            Console.WriteLine(" help - Show this prompt");
            Console.WriteLine("");


        }

        public string Feeling()
        {
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" What are you feeling today? ");
            Console.WriteLine(" Calm ");
            Console.WriteLine(" Energetic");
            Console.WriteLine(" Powerful");
            Console.Write(" >");
            var input = Console.ReadLine()?.Trim().ToLower();
            try
            {
                input = Convert.ToString(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" An error occured : " + ex.Message);
                _logger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                return "";
            }

            if (string.IsNullOrEmpty(input)) return "";

            return input;
        }
    }
}
