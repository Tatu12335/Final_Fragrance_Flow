using fragrance_API.dtos.User;
using fragrance_API.jwt;
using Fragrance_flow_DL_VERSION_.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace fragrance_API.Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow")]
    public class AuthController : ControllerBase
    {

        

        // To access repos login() and getadminstatus() methods
        private readonly IFragranceRepo _repo;
        private readonly TokenGenerator _tokenGenerator;
        public AuthController(IFragranceRepo repo, TokenGenerator tokenGenerator)
        {
            _repo = repo;
            _tokenGenerator = tokenGenerator;
        }
        //
        [Route("Login")]
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

                

            if (IsAdmin == null) return Ok(new { token = token , role = "User"});

                return Ok(new { token = token , role = "Admin"});
            
           

        }
    }
}
