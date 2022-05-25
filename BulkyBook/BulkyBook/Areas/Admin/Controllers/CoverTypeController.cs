using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CoverTypeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCategoryList = _uow.CoverType.GetAll();
            return View(objCategoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _uow.CoverType.Add(coverType);
                _uow.Save();
                TempData["Success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) NotFound();

            var coverType = _uow.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (coverType == null) return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _uow.CoverType.Update(coverType);
                _uow.Save();
                TempData["Success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }

            return View(coverType);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) NotFound();

            var coverType = _uow.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (coverType == null) return NotFound();

            return View(coverType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int? id)
        {
            var coverType = _uow.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (coverType == null) return NotFound();

            _uow.CoverType.Remove(coverType);
            _uow.Save();
            TempData["Success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
