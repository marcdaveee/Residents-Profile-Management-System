using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Extensions;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Controllers
{
    [Authorize]
    public class ResidentController : Controller
    {
        private readonly IResidentRepository _residentRepository;

        private readonly IAddressRepository _addressRepository;

        private readonly IStreetRepository _streetRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ResidentController(IResidentRepository residentRepository, IAddressRepository addressRepository, IStreetRepository streetRepository, IWebHostEnvironment hostingEnvironment)
        {
            _residentRepository = residentRepository;
            _addressRepository = addressRepository;
            _streetRepository = streetRepository;
            _webHostEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index(string sortBy, string searchString, string streetId, int currentPage = 1)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            ViewData["StreetSortParm"] = sortBy == "street" ? "street_desc" : "street";
            ViewData["CurrentFilter"] = searchString;
            ViewData["StreetIdValue"] = streetId;


            // Handles Ascending and Descending Arrow Icon

            if (String.IsNullOrEmpty(sortBy))
            {
                ViewData["SortedNameIcon"] = "bi bi-arrow-down";
                ViewData["SortedStreetIcon"] = "";

                ViewData["SortByValue"] = "";
            }
            else if (sortBy.Equals("name_desc"))
            {
                ViewData["SortedNameIcon"] = "bi bi-arrow-up";
                ViewData["SortedStreetIcon"] = "";

                ViewData["SortByValue"] = "name_desc";
            }
            else if (sortBy.Equals("street_desc"))
            {
                ViewData["SortedStreetIcon"] = "bi bi-arrow-up";
                ViewData["SortedNameIcon"] = "";

                ViewData["SortByValue"] = "street_desc";
            }
            else
            {
                ViewData["SortedStreetIcon"] = "bi bi-arrow-down";
                ViewData["SortedNameIcon"] = "";

                ViewData["SortByValue"] = "street";
            }

            //Handles pagination
            int pageSize = 8;
            var paginatedResidents = await _residentRepository.GetAllResidents(sortBy, searchString, streetId, currentPage, pageSize);

            //calculate age
            foreach (Resident resident in paginatedResidents.Items)
            {
                resident.Age = resident.Birthday.HasValue ? (DateTime.Today.Year - resident.Birthday.Value.Year) : 0;
            }

            //Get All Streets   
            var streetList = await _streetRepository.GetAll();

            //Get Address
            var address = await _addressRepository.GetAddress();

            var residentViewModel = new ResidentViewModel
            {
                PaginatedResidents = paginatedResidents,
                Address = address,
                Streets = streetList.ToList(),
            };


            return View(residentViewModel);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var resident = await _residentRepository.GetResidentById(Id);

            resident.Age = resident.Birthday.HasValue ? (DateTime.Today.Year - resident.Birthday.Value.Year) : 0;

            if (resident == null)
            {
                return View("Error");
            }


            return View(resident);
        }

        public async Task<IActionResult> Add()
        {
            //Get Main Address
            var mainAddress = await _addressRepository.GetAddress();

            var createResidentVM = new CreateResidentViewModel
            {
                Address = mainAddress                
            };

            //Get All Streets
            var streetList = await _streetRepository.GetAll();

            if (streetList == null)
            {
                var selectListItems = new List<SelectListItem>();

                var selectListItem = new SelectListItem
                {
                    Text = "No Sreets Available",
                    Value = ""
                };

                selectListItems.Add(selectListItem);

                createResidentVM.Streets = selectListItems;
            }
            else
            {
                createResidentVM.Streets = streetList.ToSelectListItem("StreetName", "Id");
               
            }


            //ViewBag.MainAddress = mainAddress;

            //var streetList = mainAddress.Streets.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.StreetName }).ToList();
            //if (streetList == null)
            //{
            //    ViewBag.Streets = new SelectListItem { Value = null, Text = "No Streets Available" };
            //}
            //else
            //{
            //    ViewBag.Streets = streetList;
            //}


            return View(createResidentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateResidentViewModel newResident)
        {
            if (!ModelState.IsValid)
            {                
              
                //Ensure that DoB is not more than the current year
                if (newResident.Birthday > DateTime.Now)
                {
                    ModelState.AddModelError(nameof(newResident.Birthday), "Birthday is invalid.");

                }

                //Get Main Address
                var mainAddress = await _addressRepository.GetAddress();

                //Get All Streets
                var streetList = await _streetRepository.GetAll();

                newResident.Streets = streetList.ToSelectListItem("StreetName", "Id");
                newResident.Address = mainAddress;
                

                return View(newResident);
            }

            string uniqueFileName = null;

            if (newResident.Photo != null)
            {
                string imageUploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + '_' + newResident.Photo.FileName;
                string filePath = Path.Combine(imageUploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newResident.Photo.CopyTo(fileStream);
                }

            }

            var residentModel = new Resident
            {
                Firstname = newResident.Firstname,
                Lastname = newResident.Lastname,
                Middlename = newResident.Middlename,
                PhotoPath = uniqueFileName,
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

            if (resident == null)
            {
                return View("Error");
            }

            var residentToEdit = new EditResidentViewModel
            {
                Id = resident.Id,
                Firstname = resident.Firstname,
                Lastname = resident.Lastname,
                Middlename = resident.Middlename,
                ExistingPhotoPath = resident.PhotoPath,
                Age = resident.Birthday.HasValue ? (DateTime.Today.Year - resident.Birthday.Value.Year) : 0,
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

            foreach (var street in streetList)
            {
                if (street.Value == id.ToString())
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

                //Ensure that DoB is not more than the current year
                if (updatedResident.Birthday > DateTime.Now)
                {
                    ModelState.AddModelError(nameof(updatedResident.Birthday), "Birthday is invalid.");

                }
                
                return View(updatedResident);
            }

            string uniqueFileName = null;

            //Process update of image
            if (updatedResident.Photo != null)
            {

                //Ensure that uploaded file is an image

                var allowedImgExensions = new[] { ".jpg", ".jpeg", ".png" };
                Console.WriteLine(updatedResident.Photo.FileName);
                if (!allowedImgExensions.Contains(Path.GetExtension(updatedResident.Photo.FileName).ToLower()))
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

                    ModelState.AddModelError(nameof(updatedResident.Photo), "Must be an image.");

                    return View(updatedResident);
                }
                else
                {
                    string imageUploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + updatedResident.Photo.FileName;
                    string filePath = Path.Combine(imageUploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        updatedResident.Photo.CopyTo(fileStream);
                    }

                    //Update Resident Model photo file name with the new photo file name
                    residentModel.PhotoPath = uniqueFileName;

                    //Delete existing image in the image upload folder
                    if (updatedResident.ExistingPhotoPath != null)
                    {
                        string existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", updatedResident.ExistingPhotoPath);
                        System.IO.File.Delete(existingFilePath);
                    }
                }

            }          

       
           
            var updateResult = await _residentRepository.Update(residentModel, updatedResident);

            if (updateResult == false)
            {
                // Handle failed updates
                return RedirectToAction("Details", new { Id = id });
            }


            return RedirectToAction("Details", new { Id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var residentModel = await _residentRepository.GetResidentById(id);

            if (residentModel == null)
            {
                return View("Error");
            }

            if (residentModel.PhotoPath != null)
            {
                string existingPhotoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", residentModel.PhotoPath);
                System.IO.File.Delete(existingPhotoPath);
            }


            await _residentRepository.Delete(residentModel);
            return RedirectToAction("Index");
        }


    }
}
