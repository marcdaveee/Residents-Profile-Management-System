using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Controllers
{
    public class ResidentController : Controller
    {        
        private readonly IResidentRepository _residentRepository;

        private readonly IAddressRepository _addressRepository;

        public ResidentController(IResidentRepository residentRepository, IAddressRepository addressRepository)
        {            
            _residentRepository = residentRepository;
            _addressRepository = addressRepository;
        }
        public async Task<IActionResult> Index(string sortBy, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            ViewData["StreetSortParm"] = sortBy == "street" ? "street_desc" : "street";
            ViewData["CurrentFilter"] = searchString;

            // Handles Ascending and Descending Arrow Icon

            if(String.IsNullOrEmpty(sortBy))
            {
                ViewData["SortedNameIcon"] = "bi bi-arrow-down";
                ViewData["SortedStreetIcon"]= "";
            }
            else if (sortBy.Equals("name_desc"))
            {
                ViewData["SortedNameIcon"] = "bi bi-arrow-up";
                ViewData["SortedStreetIcon"] = "";
            }
            else if (sortBy.Equals("street_desc"))
            {
                ViewData["SortedStreetIcon"] = "bi bi-arrow-up";
                ViewData["SortedNameIcon"] = "";                
            }
            else
            {
                ViewData["SortedStreetIcon"] = "bi bi-arrow-down";
                ViewData["SortedNameIcon"] = "";
            }

            var residents = await _residentRepository.GetAllResidents(sortBy, searchString);

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

        public async Task<IActionResult> Add()
        {
            var mainAddress = await _addressRepository.GetAddress();

            ViewBag.MainAddress = mainAddress;

            var streetList = mainAddress.Streets.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StreetName }).ToList();            
            if(streetList == null)
            {
                ViewBag.Streets = new SelectListItem { Value = null, Text = "No Streets Available" };
            }
            else
            {
                ViewBag.Streets = streetList;
            }
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateResidentViewModel newResident)
        {
            if (!ModelState.IsValid)
            {
                var mainAddress = await _addressRepository.GetAddress();

                var streetList = mainAddress.Streets.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StreetName }).ToList();
                ViewBag.Streets = streetList;

                ViewBag.MainAddress = mainAddress;

                //Ensure that DoB is not more than the current year
                if (newResident.Birthday > DateTime.Now)
                {
                    ModelState.AddModelError(nameof(newResident.Birthday), "Birthday is invalid.");
                 
                }

                return View(newResident);
            }

            

            var residentModel = new Resident
            {
                Firstname = newResident.Firstname,
                Lastname = newResident.Lastname,
                Middlename = newResident.Middlename,
                Age = newResident.Age,
                Gender = newResident.Gender,
                Status = newResident.Status,
                Birthday = newResident.Birthday,
                ContactNo = newResident.ContactNo,
                Email = newResident.Email,
                AddressId = newResident.AddressId,
                StreetId = Convert.ToInt32(newResident.StreetId),
            };

            await _residentRepository.Add(residentModel);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var resident = await _residentRepository.GetResidentById(id);
                                   
            if(resident == null)
            {
                return View("Error");
            }

            var residentToEdit = new EditResidentViewModel
            {
                Id = resident.Id,
                Firstname = resident.Firstname,
                Lastname = resident.Lastname,
                Middlename = resident.Middlename,
                Age = resident.Age,
                Gender = resident.Gender,
                Status = resident.Status,
                Birthday = resident.Birthday,
                ContactNo = resident.ContactNo,
                Email = resident.Email,
                AddressId = resident.AddressId,
                StreetId = Convert.ToInt32(resident.StreetId),                
            };

            var mainAddress = await _addressRepository.GetAddress();
            ViewBag.MainAddress = mainAddress;

            var streetList = mainAddress.Streets.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StreetName, Selected = false }).ToList();
            
            foreach(var street in streetList)
            {
                if(street.Value == id.ToString())
                {
                    street.Selected = true;
                }
            }

            ViewBag.StreetList = streetList;

            return View("Edit", residentToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditResidentViewModel updatedResident)
        {

            var residentModel = await _residentRepository.GetResidentById(id);

            if (residentModel == null)
            {
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                var mainAddress = await _addressRepository.GetAddress();
                ViewBag.MainAddress = mainAddress;

                var streetList = mainAddress.Streets.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StreetName, Selected = false }).ToList();

                foreach (var street in streetList)
                {
                    if (street.Value == id.ToString())
                    {
                        street.Selected = true;
                    }
                }

                ViewBag.StreetList = streetList;
                return View(updatedResident);
            }

            var updateResult = await _residentRepository.Update(residentModel, updatedResident);

            if(updateResult == false)
            {
                return View("Error");
            }
          

            return RedirectToAction("Details", new { Id = id});
        }

        public async Task<IActionResult> Delete(int id)
        {
            var residentModel = await _residentRepository.GetResidentById(id);

            if (residentModel == null)
            {
                return View("Error");
            }


            await _residentRepository.Delete(residentModel);
            return RedirectToAction("Index");
        }
    }
}
