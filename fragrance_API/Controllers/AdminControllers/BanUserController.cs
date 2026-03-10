using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]
    public class BanUserController : Controller
    {
        
        private readonly IAdminServices _adminServices;
        public BanUserController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        public class BanDto
        { 
            public int id { get; set; }
        }
        [HttpPatch("Ban")]
        public async Task<IActionResult> BanUser([FromBody]BanDto dto)
        {
            try
            {
                await _adminServices.BanUserById(dto.id);
                return Ok(new { message = " Banned user successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception($" An error occured banning user : {ex.Message}");
            }
        }
    }
}
