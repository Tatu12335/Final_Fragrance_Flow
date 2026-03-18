using fragrance_API.dtos.Auth;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers
{
    public class FragranceController : Controller
    {
        // To access the fragrance repos addFragrance() METHod 
        private readonly IFragranceRepo _repo;
        public FragranceController(IFragranceRepo repo)
        {
            _repo = repo;
        }
        [Authorize]
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
                return StatusCode(500, new { message = $"Error adding fragrance: {ex.Message}" });
            }

            
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string username, int id)
        {
            var userId = await _repo.GetUserId(username);

            try
            {
                await _repo.RemoveFragranceById(userId.id, id);
                return Ok(new { message = " Remove successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500);

            }
        }
        [Authorize]
        [HttpGet("GetAllFragrances")]
        public async Task<IActionResult> Get()
        {
            var fragrances = await _repo.GetAllAsync();
            return Ok(fragrances);
        }
        [Authorize]
        [HttpGet("GetUsers")]
        public async Task<Users> GetUserInfoByUsername(string username)
        {

            var userInfo = _repo.CheckIfUserExists(username);
            if (userInfo == null)
            {
                return null;
            }
            return await (userInfo);
        }
        [Authorize]
        [Route("Fragrances")]
        [HttpPost]
        public async Task<IEnumerable<Fragrance>> GetFragrancesByUserId([FromBody] UserIdRequest id)
        {
            Users userInfo = await _repo.CheckIfUserExists(id.username);
            if (userInfo == null)
            {
                return null;
            }
            var fragrances = await _repo.GetFragrancesByUserId(id.username, userInfo.id);
            return fragrances;
        }
    }
}
