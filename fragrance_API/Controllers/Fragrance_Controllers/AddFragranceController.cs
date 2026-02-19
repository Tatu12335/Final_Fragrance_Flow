using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddFragrance([FromBody] Fragrance fragrance, string username)
        {
            try
            {
                if (fragrance == null) throw new Exception("userid is null, did you try to add a fragrance to a user that doesnt exist");

                await _repo.AddFragrance(username, fragrance);

                return Ok(new { message = "Fragrance added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { message = $"Error adding fragrance: {ex.Message}" });
            }

        }
    }
}
