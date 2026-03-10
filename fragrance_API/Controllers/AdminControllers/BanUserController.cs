using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]
    public class BanUserController : ControllerBase
    {
        // To access the admin functionality
        private readonly IAdminServices _adminServices;
        public BanUserController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        // I know this dto shouln't be here but it work so imma keep it.
        public class BanDto
        {
            public int id { get; set; }
        }
        [HttpPatch("Ban")]
        public async Task<IActionResult> BanUser([FromBody] BanDto dto)
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
