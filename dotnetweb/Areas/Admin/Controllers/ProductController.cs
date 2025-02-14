using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModel;

namespace bookshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // construct the dependency injection
        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }

        // show List
        public IActionResult Index()
        {
            List<Product> objProductData = _unitOfWork.Product.GetAll().ToList();
            return View(objProductData);
        }

        // show Update and Create 
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {

                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            // create
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            // update
            else
            {
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        // save Update and Create 
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                using (var stramFile = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(stramFile);
                }
                productVM.Product.ImageUrl = @"\images\product" + fileName;
            }
            else
            {
                productVM.Product.ImageUrl = "";
            }
            // validations
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully.";
                return RedirectToAction("Index");
            }
            // return the Upsert View
            else
            {
                ProductVM ProductVM = new ProductVM()
                {
                    CategoryList = _unitOfWork.Category
                        .GetAll().Select(u => new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        }),
                    Product = new Product()
                };
                return View(ProductVM);
            }
        }

        // Show for deletion
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //to match with the primary key
            Product? categoryData = _unitOfWork.Product.Get(u => u.Id == id);

            if (categoryData == null)
            {
                return NotFound();
            }

            return View(categoryData);
        }

        // Delete Product
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}