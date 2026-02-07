using Microsoft.AspNetCore.Mvc;
using Fragrance_flow_DL_VERSION_.models;
using Fragrance_flow_DL_VERSION_.classes;
using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.Identity.Client;

namespace fragrance_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FragranceController : Controller
    {
        private readonly string _connectionString;
        private readonly IFragranceRepo _repository;

        public FragranceController(string connectionString, IFragranceRepo repository)
        {
            _connectionString = connectionString;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetFrag()
        {
            var fragrances = await _repository.GetAllAsync();
            return Ok(fragrances);
        }
    }
}
