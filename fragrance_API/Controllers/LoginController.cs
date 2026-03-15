using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fragrance_API.Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Login")]
    public class FragranceController : ControllerBase
    {
        // Same with this. It should not be here 
        public class User
        {
            [Required]
            [JsonPropertyName("username")]
            public string username { get; set; }

            [Required]
            [JsonPropertyName("password")]
            public string password { get; set; }
        }
        //
        // To access repos login() and getadminstatus() methods
        private readonly IFragranceRepo _repo;
        public FragranceController(IFragranceRepo repo)
        {
            _repo = repo;
        }
        //
        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] User user)
        {
            var userEntity = await _repo.Login(user.username, user.password);
            
            

            var IsAdmin = await _repo.GetAdminStatus(user.username);
            if (userEntity == null)
            {
                return NotFound($" Error occured : User not found ");
            }

            if (IsAdmin == null) return Ok(new { message = " Successfully logged in, Not admin" });

            return Ok(new { message = " Succesfully logged in, user is admin" });

        }

    }
}
