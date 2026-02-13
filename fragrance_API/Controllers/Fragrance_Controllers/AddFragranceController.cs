using Microsoft.AspNetCore.Mvc;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using System.Threading.Tasks;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Fragrances")]
    public class AddFragranceController : Controller
    {
        private readonly IFragranceRepo _repo;
        
        public AddFragranceController(IFragranceRepo repo)
        {
            _repo = repo;      
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddFragrance([FromBody] Fragrance fragrance)
        {
            try
            {
                var user = HttpContext.User.Identity.Name; 
                if(string.IsNullOrEmpty(user))
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                await _repo.AddFragrance(user, fragrance);
                return Ok(new { message = "Fragrance added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error adding fragrance: {ex.Message}" });
            }
                
        }
    }
}
