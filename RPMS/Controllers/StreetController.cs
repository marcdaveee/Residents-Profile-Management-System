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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateStreetViewModel newStreet)
        {
            if (ModelState.IsValid)
            {
                var streetModel = new Street
                {
                    StreetName = newStreet.StreetName,
                    AddressId = 1   
                };

                await _streetRepo.AddStreet(streetModel);
                return RedirectToAction("Index", "Address");
                
            }
            return View(newStreet);

        }

        public async Task <IActionResult> Edit(int id)
        {
            var streetModel = await _streetRepo.GetById(id);

            if (streetModel == null)
            {
                return View("Error");
            }            

            var modelToEdit = new EditStreetViewModel
            {
                StreetName = streetModel.StreetName,
                AddressId = streetModel.AddressId,
            };

            return View(modelToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditStreetViewModel updatedStreet)
        {
            var streetModel = await _streetRepo.GetById(id);

            if(streetModel == null)
            {
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                return View(updatedStreet);
            }

            await _streetRepo.Edit(streetModel, updatedStreet);

            return RedirectToAction("Index", "Address");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var streetModel = await _streetRepo.GetById(id);

            if (streetModel == null)
            {
                return View("Error");
            }            

            await _streetRepo.Delete(streetModel);

            return RedirectToAction("Index", "Address");

        }
        
    }
}
