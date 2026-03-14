using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]
    public class PrometeController : Controller
    {
        public class PromoteDto
        {
            [Required]
            public int id {  get; set; }
        }
        private readonly IAdminServices _adminService;
        // TO : DO Better error handling and move all dto's from the controllers to models/Dtos folder
        public PrometeController(IAdminServices adminService)
        {
            _adminService = adminService;
        }

        [HttpPatch("Promote")]
        public async Task <IActionResult> PromoteUser([FromBody] PromoteDto dto)
        {
            try
            {
                await _adminService.PromoteUserById(dto.id);
                return Ok(new {message = $"User Promoted to admin " });
            }
            catch (NullReferenceException ex)
            { 
                return BadRequest(" Users id is null, Message > " + ex.Message);    
         
            }
            catch(Exception ex)
            {
                return BadRequest(" Unexpected error occured, Message > " + ex.Message);  
            }
        }
    }
}
