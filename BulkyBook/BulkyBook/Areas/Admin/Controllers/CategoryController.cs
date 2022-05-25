using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _uow.Category.GetAll();
            return View(objCategoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _uow.Category.Add(category);
                _uow.Save();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) NotFound();

            var category = _uow.Category.GetFirstOrDefault(x => x.ID == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _uow.Category.Update(category);
                _uow.Save();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) NotFound();

            var category = _uow.Category.GetFirstOrDefault(x => x.ID == id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int? id)
        {
            var category = _uow.Category.GetFirstOrDefault(x => x.ID == id);
            if (category == null) return NotFound();

            _uow.Category.Remove(category);
            _uow.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }

    }
}
