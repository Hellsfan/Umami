using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umami.Models.Domain;
using Umami.Services.Interfaces;

namespace Umami.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = productService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = productService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Product model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = productService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(ProductList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult ProductList()
        {
            var data = this.productService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = productService.Delete(id);
            return RedirectToAction(nameof(ProductList));
        }
    }
}
