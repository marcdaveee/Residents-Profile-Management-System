using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPMS.Data;

namespace RPMS.Controllers
{
    public class ResidentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidentController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var residents = _context.Residents.Include(r => r.Address).ToList();
            return View(residents);
        }

        public IActionResult Details(int Id)
        {
            var resident = _context.Residents.Include(r => r.Address).SingleOrDefault(r => r.Id == Id);

            return View(resident);
        }
    }
}
