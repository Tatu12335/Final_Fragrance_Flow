using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace fragrance_API.Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Login")]
    public class FragranceController : ControllerBase
    {
        public class User
        {
            [JsonPropertyName("username")]
            public string username { get; set; }

            [JsonPropertyName("password")]
            public string password { get; set; }
        }
        private readonly IFragranceRepo _repo;
        public FragranceController(IFragranceRepo repo) 
        { 
            _repo = repo;
        }

        [HttpPost]
        public async Task <IActionResult> GetUser([FromBody] User user)
        { 
            var userEntity = await _repo.Login(user.username,user.password);
            if (userEntity == null)
            {
                return BadRequest($" Error occured ");
            }
            return Ok (new {message =" Sucessfully logged in"});

        }
       
    }
}
