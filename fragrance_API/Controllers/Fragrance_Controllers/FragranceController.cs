using Microsoft.AspNetCore.Mvc;
using Fragrance_flow_DL_VERSION_.models;
using Fragrance_flow_DL_VERSION_.classes;
using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.Identity.Client;

namespace fragrance_API.Controllers.Fragrance_Controllers
{
    [ApiController]
    [Route("api/Fragrance_Flow")]
    public class FragranceController : ControllerBase
    {
        private readonly IFragranceRepo _repository;

        public FragranceController(IFragranceRepo repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fragrances = await _repository.GetAllAsync();
            return Ok(fragrances);
        }
        /*[HttpGet]
        public async Task<IActionResult> GetUserFragrances(string username, int id)
        {
            var fragrances = await _repository.GetFragrancesByUserId(username, id);
            return Ok(fragrances);

        }*/









    }
}
