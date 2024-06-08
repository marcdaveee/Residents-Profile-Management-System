using Microsoft.AspNetCore.Mvc;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Controllers
{
    public class StreetController : Controller
    {
        private IStreetRepository _streetRepo;

        public StreetController(IStreetRepository streetRepo)
        {
            _streetRepo = streetRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateStreetVM newStreet)
        {
            if (!ModelState.IsValid)
            {
                return View(newStreet);
            }

            var streetModel = new Street
            {
                StreetName = newStreet.StreetName
            };

            await _streetRepo.AddStreet(streetModel);
            return View("Address");
        }
    }
}
