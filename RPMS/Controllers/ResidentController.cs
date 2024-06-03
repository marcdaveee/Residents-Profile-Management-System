using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;

namespace RPMS.Controllers
{
    public class ResidentController : Controller
    {        
        private readonly IResidentRepository _residentRepository;

        public ResidentController(IResidentRepository residentRepository)
        {            
            _residentRepository = residentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var residents = await _residentRepository.GetAllResidents();

            return View(residents);
        }

        public async Task <IActionResult> Details(int Id)
        {
            var resident = await _residentRepository.GetResidentById(Id);

            if(resident == null)
            {
                return View("Error");
            }

            return View(resident);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Resident newResident)
        {
            if (!ModelState.IsValid)
            {
                return View(newResident);
            }

            await _residentRepository.Add(newResident);

            return RedirectToAction("Index");
        }
    }
}
