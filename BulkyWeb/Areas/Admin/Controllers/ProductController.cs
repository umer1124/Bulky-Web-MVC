using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ROLE_ADMIN)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> list = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(list);
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new() 
            { 
                CategoryList = _unitOfWork.Category
                .GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                }),
                Product = (id == null || id == 0) ? new Product() : _unitOfWork.Product.Get(item => item.Id == id, includeProperties: "ProductImages")
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                    TempData["success"] = "Product updated successfully";
                }

                _unitOfWork.Save();

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = @"images\products\product-" + productViewModel.Product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, filename), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        ProductImage productImage = new()
                        {
                            ProductId = productViewModel.Product.Id,
                            ImageUrl = @"\" + productPath + @"\" + filename
                        };

                        if (productViewModel.Product.ProductImages == null)
                        {
                            productViewModel.Product.ProductImages = new List<ProductImage>();
                        }

                        productViewModel.Product.ProductImages.Add(productImage);
                    }

                    _unitOfWork.Product.Update(productViewModel.Product);
                    _unitOfWork.Save();
                }
                                
                return RedirectToAction("Index");
            }

            productViewModel.CategoryList = _unitOfWork.Category
                .GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                });

            return View(productViewModel);
        }

        public IActionResult DeleteImage(int imageId)
        {
            var imageToDelete = _unitOfWork.ProductImage.Get(item => item.Id == imageId);
            if (imageToDelete != null)
            {
                if (!string.IsNullOrEmpty(imageToDelete.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                        imageToDelete.ImageUrl.TrimStart('\\')
                    );

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    _unitOfWork.ProductImage.Remove(imageToDelete);
                    _unitOfWork.Save();

                    TempData["success"] = "Delete successfully";
                }

                return RedirectToAction(nameof(Upsert), new { id = imageToDelete.ProductId });
            }

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> list = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = list });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToDelete = _unitOfWork.Product.Get(item => item.Id == id);
            if (productToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, productPath);
            if ( Directory.Exists(finalPath)) 
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }

            _unitOfWork.Product.Remove(productToDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Product deleted successfully" });
        }

        #endregion
    }
}
