using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]

    public class UnbanUserController : Controller
    {
        public class UnbanDto
        {
            [Required]
            public int id { get; set; }
        }
        public readonly IAdminServices _adminService;
        public UnbanUserController(IAdminServices adminServices)
        {
            _adminService = adminServices;
        }

        [HttpPatch("Unban")]
        public async Task<IActionResult> UnbanUserAsync([FromBody]UnbanDto dto)
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
