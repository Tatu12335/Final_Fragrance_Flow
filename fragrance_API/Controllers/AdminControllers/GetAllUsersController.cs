using Microsoft.AspNetCore.Mvc;

namespace fragrance_API.Controllers.AdminControllers
{
    public class GetAllUsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
