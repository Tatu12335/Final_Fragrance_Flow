using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Fragrance_flow_DL_VERSION_.models.dtos;

namespace fragrance_API.Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Login")]
    public class FragranceController : ControllerBase
    {
      
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
