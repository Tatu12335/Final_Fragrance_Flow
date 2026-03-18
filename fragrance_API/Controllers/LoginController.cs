using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Fragrance_flow_DL_VERSION_.models.dtos;
using Microsoft.AspNetCore.Authorization;
using fragrance_API.jwt;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace fragrance_API.Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Login")]
    public class FragranceController : ControllerBase
    {
      
        // To access repos login() and getadminstatus() methods
        private readonly IFragranceRepo _repo;
        private readonly TokenGenerator _tokenGenerator;
        public FragranceController(IFragranceRepo repo,TokenGenerator tokenGenerator)
        {
            _repo = repo;
            _tokenGenerator = tokenGenerator;
        }
        //
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetUser([FromBody] User user)
        {           
            var userEntity = await _repo.Login(user.username, user.password);

            var IsAdmin = await _repo.GetAdminStatus(user.username);
            
            if (userEntity == null)
            {
                return NotFound($" Error occured : User not found ");
            }
            // If UserEntity is not null generate token
            var token = _tokenGenerator.GenerateToken(userEntity);


            if (IsAdmin == null) return Ok(new { message = " Successfully logged in, Not admin" });
            
            return Ok(new { message = " Succesfully logged in, user is admin" });

        }
    }
}
