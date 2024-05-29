using Microsoft.AspNetCore.Mvc;

namespace RPMS.Controllers
{
    public class ResidentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
