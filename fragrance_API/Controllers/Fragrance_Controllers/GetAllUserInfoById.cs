using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow/Users")]
    public class GetAllUserInfoById : ControllerBase
    {
        private readonly IFragranceRepo _repo;
        public GetAllUserInfoById(IFragranceRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]

        public async Task<Users> GetUserInfoByUsername(string username)
        {

            var userInfo = _repo.CheckIfUserExists(username);
            if (userInfo == null)
            {
                return null;
            }
            return await (userInfo);
        }
    }
}