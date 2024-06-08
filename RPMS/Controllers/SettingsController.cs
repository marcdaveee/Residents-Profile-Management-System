using Microsoft.AspNetCore.Mvc;

namespace RPMS.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
