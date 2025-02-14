using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace bookshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // construct the dependency injection
        public CategoryController(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;
        }

        // show the list
        public IActionResult Index()
        {
            List<Category> objCategoryData = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryData);
        }

        // show the Category form
        public IActionResult Create()
        {
            return View();
        }
        // save the Category form
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // custom validations
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The value of the Name field exactly matches the Display Order.");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test isn't a Valid Value.");
            }


            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        // edit the Cateory Data
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //to match with the primary key
            Category? categoryData = _unitOfWork.Category.Get(u => u.Id == id);

            //Category? categoryData2 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryData3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryData == null)
            {
                return NotFound();
            }

            return View(categoryData);
        }

        // Save the Cateory Data
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        // Show the Cateory Data
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //to match with the primary key
            Category? categoryData = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryData == null)
            {
                return NotFound();
            }

            return View(categoryData);
        }

        // Delete the Cateory Data
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Category Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}
