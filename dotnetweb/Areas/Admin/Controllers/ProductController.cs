using System.Collections;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
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
            List<Product> objProductData = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
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
            // validations
            if (ModelState.IsValid)
            {
                // it's autometicaly identify the path of hosted app
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                // check if file is exist
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    // delete old image
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(productPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    // save the image into this folder "images/product/"
                    using (var stramFile = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(stramFile);
                    }
                    // bind the model property for inserting Database
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                // check if id == 0 Add
                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product Created Successfully.";
                }
                // else if id != 0 Update
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                    TempData["success"] = "Product Updated Successfully.";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            // else return the Upsert View
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


        #region API CALLS

        // Get All products
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductData = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { success = true, data = objProductData });
        }

        // Delete a Product
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Data not found" });
            }
            if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
            {
                string imgPath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Successfully Delete" });
        }
        #endregion
    }
}