using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;

namespace RPMS.Controllers
{
    public class ResidentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IResidentRepository _residentRepository;

        public ResidentController(ApplicationDbContext context, IResidentRepository residentRepository)
        {
            _context = context;
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
    }
}
