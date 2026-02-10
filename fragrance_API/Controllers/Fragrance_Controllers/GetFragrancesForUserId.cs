using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow")]
    public class GetFragrancesForUserId : Controller
    {
        private readonly IFragranceRepo _repo;
        public GetFragrancesForUserId(IFragranceRepo repo)
        {
            _repo = repo;
        }
        [HttpGet("Fragrances")]
        public async Task<IEnumerable<Fragrance>> GetFragrancesByUserId(string username)
        {
            Users userInfo = await _repo.CheckIfUserExists(username);
            if (userInfo == null)
            {
                return null;
            }
            var fragrances = await _repo.GetFragrancesByUserId(username, userInfo.id);
            return fragrances;
        }
    }
}
