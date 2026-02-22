using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;

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

                if (adminStatus == null) return NoContent();


                return Ok(new { message = " User is Admin" });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
