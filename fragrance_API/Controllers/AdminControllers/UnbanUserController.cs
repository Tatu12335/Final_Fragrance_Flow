using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;
using Fragrance_flow_DL_VERSION_.models.dtos;
using Microsoft.AspNetCore.Authorization;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]

    public class UnbanUserController : ControllerBase
    {
       
       
        // To access the admin functionality from the repo
        public readonly IAdminServices _adminService;
        public UnbanUserController(IAdminServices adminServices)
        {
            _adminService = adminServices;
        }
        //
        
        [HttpPatch("Unban")]
        public async Task<IActionResult> UnbanUserAsync([FromBody] UnbanDto dto)
        {
            try
            {
                await _adminService.UnbanUserById(dto.id);
                return Ok(new { message = " Successfully unbanned user" });
            }
            catch (Exception ex)
            {
                return BadRequest($" Fatal error occured : {ex.Message}");
            }
        }
    }
}
