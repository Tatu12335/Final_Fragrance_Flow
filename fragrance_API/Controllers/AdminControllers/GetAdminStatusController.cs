using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users")]
    public class GetAdminStatusController : Controller
    {
        private readonly IFragranceRepo _repository;

        public GetAdminStatusController(IFragranceRepo repository)
        {
            _repository = repository;
        }

        [HttpGet("IsAdmin")]
        public async Task<IActionResult> GetAdminStatus(string username)
        {
            try
            {
                var adminStatus = await _repository.GetAdminStatus(username);
                
                if (adminStatus == null) return Ok(new { message = " User is not admin" });
                
                
                return Ok(new { message = $" User is admin" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
