using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Users")]
    public class CreateUser : ControllerBase
    {
        private readonly IFragranceRepo _repo;
        public CreateUser(IFragranceRepo repo)
        {
            _repo = repo;
        }
        /* public IActionResult Index()
         {
             return View();
         }*/

        [Route("api/Fragrance_Flow/Users/Create")]
        [HttpPost]
        public async Task<UserSession> Create( string username, string email, [FromBody] string password)
        {
            try
            {
                var userEntity = await _repo.CheckIfUserExists(username);

                

                var newUser = await _repo.CreateNewUserAsync(username, email, password);

                return newUser;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
        }
    }
}