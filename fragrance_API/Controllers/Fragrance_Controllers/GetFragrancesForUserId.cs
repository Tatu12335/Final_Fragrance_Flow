using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Fragrance_flow_DL_VERSION_.models.dtos;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow")]
    public class GetFragrancesForUserId : Controller
    {
        
        // To access repos both checkifuserexist() and getfragrancebyuserid()
        private readonly IFragranceRepo _repo;
        public GetFragrancesForUserId(IFragranceRepo repo)
        {
            _repo = repo;
        }


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
