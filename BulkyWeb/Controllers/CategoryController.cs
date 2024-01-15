using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;    
        }

        public IActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll().ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name != null && category.Name == category.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }

            if (ModelState.IsValid) 
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();

                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }            

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            Category? category = _categoryRepository.Get(item => item.Id == id);

            if (category == null) 
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();

                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? category = _categoryRepository.Get(item => item.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _categoryRepository.Get(item => item.Id == id);

            if (category == null) 
            {
                return NotFound();
            }

            _categoryRepository.Remove(category);
            _categoryRepository.Save();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
