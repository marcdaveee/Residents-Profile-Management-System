using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Controllers
{
    [Authorize]
    public class StreetController : Controller
    {
        private IStreetRepository _streetRepo;
        private readonly IAddressRepository _addressRepo;

        public StreetController(IStreetRepository streetRepo, IAddressRepository addressRepo)
        {
            _streetRepo = streetRepo;
            _addressRepo = addressRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Add()
        {
            var addressModel = await _addressRepo.GetAddress();


            var createStreetViewModel = new CreateStreetViewModel
            {
                StreetName = "",
                AddressId = addressModel.Id,
            };
            return View(createStreetViewModel);
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
                    AddressId = newStreet.AddressId   
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
