using fragrance_API.dtos.Admin;
using Fragrance_flow_DL_VERSION_.classes.Services;
using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers
{
    public class AdminController : ControllerBase
    {
        // To access the admin functionality
        private readonly IAdminServices _adminServices;
        private readonly IFragranceRepo _repository;
        public AdminController(IAdminServices adminServices, IFragranceRepo repository)
        {
            _adminServices = adminServices;
            _repository = repository;
        }

        [Authorize("Admin")]
        [HttpPatch("Ban")]
        public async Task<IActionResult> BanUser([FromBody] BanDto dto)
        {
            await _adminServices.BanUserById(dto.id);
            return Ok(new { message = " Banned user successfully" });
        }
        
        [HttpGet("IsAdmin")]
        public async Task<IActionResult> GetAdminStatus(string username)
        {
            try
            {
                var adminStatus = await _repository.GetAdminStatus(username);

                if (adminStatus == null) return NoContent();
                
                return Ok(new { message = " User is Admin" });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Authorize("Admin")]
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
        [Authorize("Admin")]
        [HttpPatch("Promote")]
        public async Task<IActionResult> PromoteUser([FromBody] PromoteDto dto)
        {
            try
            {
                await _adminServices.PromoteUserById(dto.id);
                return Ok(new { message = $"User Promoted to admin " });
            }
            catch (Exception ex)
            {
                return BadRequest(" Unexpected error occured, Message > " + ex.Message);
            }
        }
        [Authorize("Admin")]
        [HttpPatch("Unban")]
        public async Task<IActionResult> UnbanUserAsync([FromBody] UnbanDto dto)
        {
            try
            {
                await _adminServices.UnbanUserById(dto.id);
                return Ok(new { message = " Successfully unbanned user" });
            }
            catch (Exception ex)
            {
                return BadRequest($" Fatal error occured : {ex.Message}");
            }
        }
    }
}
