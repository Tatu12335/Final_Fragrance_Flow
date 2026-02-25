using Fragrance_flow_DL_VERSION_.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.AdminControllers
{
    [Controller]
    [Route("api/Fragrance_Flow/Users/Admin")]
    public class BanUserController : Controller
    {
        private readonly IAdminServices _adminServices;
        public BanUserController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }
        [HttpPatch]
        public async Task <IActionResult> BanUser()
        {

        }
    }
}
