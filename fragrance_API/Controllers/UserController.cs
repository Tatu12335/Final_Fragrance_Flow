using fragrance_API.dtos.User;
using Fragrance_flow_DL_VERSION_.Application.interfaces;
using Microsoft.AspNetCore.Mvc;


namespace fragrance_API.Controllers
{
    [Route("api/Fragrance_Flow/User")]
    public class UserController : Controller
    {
        private readonly IFragranceRepo _repo;
        public UserController(IFragranceRepo repo) 
        {
            _repo = repo;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest data)
        {
            if (data == null || string.IsNullOrEmpty(data.password)) return BadRequest(" Missing information");
            try
            {
                var user = await _repo.CreateNewUserAsync(data.username, data.email, data.password);

                return Ok(new { message = " User creation successfull!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, " Api error occured while creating a new user");


            }


        }
    }
}
