using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users")]
    public class GetAllUsersController : ControllerBase
    {
        // IMPORTANT : Implement jwt later 
        // Because this is not very safe :(
        private readonly IAdminServices _adminServices;
        private readonly IFragranceRepo _repo;

        public GetAllUsersController(IFragranceRepo repo, IAdminServices adminServices)
        {
            _repo = repo;
            _adminServices = adminServices;
        }

        [HttpGet("UserList")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _adminServices.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $" Error getting the userlist : {ex.Message}" });

            }
        }
    }
}
