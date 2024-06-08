using Microsoft.AspNetCore.Mvc;
using RPMS.Interfaces;

namespace RPMS.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepo;

        public AddressController(IAddressRepository addressRepo)
        {
            _addressRepo = addressRepo;
        }
        public async Task<IActionResult> Index()
        {
            var address = await _addressRepo.GetAddress();
            

            return View(address);
        }
        

    }
}
