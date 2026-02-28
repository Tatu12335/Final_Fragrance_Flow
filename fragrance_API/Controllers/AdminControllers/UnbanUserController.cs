using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]

    public class UnbanUserController : Controller
    {
        public readonly IAdminServices _adminService;
        public UnbanUserController(IAdminServices adminServices)
        {
            _adminService = adminServices;
        }

        [HttpPatch("Unban")]
        public async Task<IActionResult> UnbanUserAsync([FromBody] int id)
        {
            try
            {
                await _adminService.UnbanUserById(id);
                return Ok(new { message = " Successfully unbanned user" });
            }
            catch (Exception ex)
            {
                return BadRequest($" Fatal error occured : {ex.Message}");
            }
        }
    }
}
