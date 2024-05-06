using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Umami.Models.ViewModels;
using Umami.Services.Implementations;
using Umami.Services.Interfaces;

namespace Umami.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IProductService productService;
        private readonly IFileService fileService;

        public RecipeController(IRecipeService _recipeService, IProductService _productService, IFileService _fileService)
        {
            recipeService = _recipeService;
            productService = _productService;
            fileService = _fileService;
        }

        public IActionResult Add()
        {
            var model = new RecipeEditorVM();

            model.ProductList = productService.List().Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(RecipeEditorVM model)
        {
            var user = User.Identity.Name;
            model.CreatedByUser = user;

            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this.fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.RecipeImage = imageName;
            }

            var recipeMapped = recipeService.MapVMtoRecipe(model);


            var result = recipeService.Add(recipeMapped);
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
            var model = recipeService.GetById(id);
            var selectedProducts = productService.GetProductsForRecipe(model.Id);
            MultiSelectList multiProductList = new MultiSelectList(productService.List(), "Id", "ProductName", selectedProducts);

            var vm = recipeService.MapRecipeToVM(model);
            vm.MultiProductList = multiProductList;

            vm.ProductList = productService.List().Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(RecipeEditorVM vm)
        {
            vm.CreatedByUser = User.Identity.Name;
            if (!ModelState.IsValid)
                return View(vm);
            if (vm.ImageFile != null)
            {
                var fileReult = this.fileService.SaveImage(vm.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(vm);
                }
                var imageName = fileReult.Item2;
                vm.RecipeImage = imageName;
            }

            var blogPost = recipeService.MapVMtoRecipe(vm);

            var result = recipeService.Update(blogPost);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(RecipeList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            var result = recipeService.Delete(id);
            return RedirectToAction(nameof(RecipeList)); ;
        }

        public IActionResult RecipeList()
        {
            var data = this.recipeService.List(keyword: User.Identity.Name);
            return View(data);
        }

        public IActionResult CookRecipe(int recipeId)
        {
            var vm = recipeService.GetCookRecipeVMbyId(recipeId);
            return View(vm);
        }

        public IActionResult CookRecipeProducts(int recipeId)
        {
            var vm = recipeService.GetCookRecipeVMbyId(recipeId);

            return View(vm);
        }

        public IActionResult CookRecipeInstructions(int recipeId)
        {
            var vm = recipeService.GetCookRecipeVMbyId(recipeId);

            return View(vm);
        }
    }
}
