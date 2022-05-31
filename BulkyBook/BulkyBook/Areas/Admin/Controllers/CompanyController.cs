using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace BulkyBook.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CompanyController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Company company = new();

            if (id == null || id == 0) 
            {
                return View(company);
            }     //create
            else 
            {
                company = _uow.Company.GetFirstOrDefault(u => u.Id == id); //update
                return View(company);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            { 
                if (company.Id == 0)
                {
                    _uow.Company.Add(company);
                    TempData["Success"] = "Company added successfully";
                } 
                else
                {
                    _uow.Company.Update(company);
                    TempData["Success"] = "Company updated successfully";
                } 

                _uow.Save();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _uow.Company.GetAll();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var company = _uow.Company.GetFirstOrDefault(x => x.Id == id);
            if (company == null) return Json(new { success = false, message = "Error while deleting" });

            _uow.Company.Remove(company);
            _uow.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion

    }

}


