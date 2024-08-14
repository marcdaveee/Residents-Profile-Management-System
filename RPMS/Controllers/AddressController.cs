using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Controllers
{

    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IStreetRepository _streetRepo;

        public AddressController(IAddressRepository addressRepo, IStreetRepository streetRepo)
        {
            _addressRepo = addressRepo;
            _streetRepo = streetRepo;
        }
        public async Task<IActionResult> Index()
        {
            var address = await _addressRepo.GetAddress();
            
            if(address == null)
            {
                return View("Create");
            }

            return View(address);
        }

        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateAddressViewModel newAddress)
        {
            if (ModelState.IsValid)
            {
                var addressModel = new Address
                {
                    Brgy = newAddress.Brgy,
                    Municipality = newAddress.Municipality,
                    Town = newAddress.Town
                };

                await _addressRepo.Add(addressModel);
                return RedirectToAction("Index");
            }
            
            return View(newAddress);
        }
        
        public async Task <IActionResult> Edit()
        {
            var address = await _addressRepo.GetAddress();

            if(address == null) {
                return View("Index");
            }

            var addressToEdit = new EditAddressViewModel
            {
                Brgy = address.Brgy,
                Municipality = address.Municipality,
                Town = address.Town
            };
            
            return View(addressToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAddressViewModel updatedAddress)
        {
            if(!ModelState.IsValid)
            {
                return View(updatedAddress);
            }
            var addressToUpdate = await _addressRepo.GetAddress();

            if (addressToUpdate == null)
            {
                return View("Index");
            }

            await _addressRepo.Update(addressToUpdate, updatedAddress);

            return RedirectToAction("Index");

        }

    }
}
