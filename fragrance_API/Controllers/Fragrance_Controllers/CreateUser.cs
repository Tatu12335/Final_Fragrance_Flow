using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Users")]
    public class CreateUser : ControllerBase
    {
        public class UserCreateRequest
        {
            [JsonPropertyName("username")]
            public string username { get; set; }
            
            [JsonPropertyName("email")]
            public string email { get; set; }
            
            [JsonPropertyName("password")]
            public string password {  get; set; }
        }
        private readonly IFragranceRepo _repo;
        public CreateUser(IFragranceRepo repo)
        {
            _repo = repo;
        }
        /* public IActionResult Index()
         {
             return View();
         }*/

        [Route("Create")]
        [HttpPost]
        public async Task <IActionResult> Create([FromBody] UserCreateRequest data)
        {
            if (data == null || string.IsNullOrEmpty(data.password)) return BadRequest(" Missing information");
            try
            {
                var user = await _repo.CreateNewUserAsync(data.username,data.email,data.password);  

                return Ok(new {message = " User creation successfull!"});
                
            }
            catch(Exception ex)
            {
                return StatusCode(500, " Api error occured while creating a new user");
                
                
            }

            
        }
        
    }

}