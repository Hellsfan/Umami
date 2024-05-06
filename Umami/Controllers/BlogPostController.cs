using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Umami.Models.Domain;
using Umami.Models.ViewModels;
using Umami.Services.Interfaces;

namespace Umami.Controllers
{
    [Authorize]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService blogPostService;
        private readonly IRecipeService recipeService;
        private readonly IFileService fileService;
        private readonly IUserAuthenticationService userAuthenticationService;

        public BlogPostController(IBlogPostService _blogPostService,
            IFileService _fileService,
            IRecipeService _recipeService,
            IUserAuthenticationService _userAuthenticationService)
        {
            blogPostService = _blogPostService;
            fileService = _fileService;
            recipeService = _recipeService;
            userAuthenticationService = _userAuthenticationService;
        }
        public IActionResult Add()
        {
            var model = new BlogPostEditorVM();

            model.RecipeList = recipeService.GetRecipesForList().Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(BlogPostEditorVM model)
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
                model.PostImage = imageName;
            }

            var blogPostMapped = blogPostService.MapVMtoBlogPost(model);


            var result = blogPostService.Add(blogPostMapped);
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
            var model = blogPostService.GetById(id);
            if (model.RecipeId != -1)
            {
                var selectedRecipe = blogPostService.GetRecipeByBlogPostId(model.Id);
                SelectList multiGenreList = new SelectList(recipeService.GetRecipesForList(), "Id", "GenreName", selectedValue: selectedRecipe);
            }
            BlogPostEditorVM vm = blogPostService.MapBlogPosttoVM(model);

            vm.RecipeList = recipeService.GetRecipesForList().Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(BlogPostEditorVM vm)
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
                vm.PostImage = imageName;
            }

            var blogPost = blogPostService.MapVMtoBlogPost(vm);

            var result = blogPostService.Update(blogPost);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(BlogPostList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(vm);
            }
        }

        public IActionResult Delete(int id)
        {
            var result = blogPostService.Delete(id);
            return RedirectToAction(nameof(BlogPostList));;
        }

        public IActionResult BlogPostList()
        {
            var data = this.blogPostService.List(searchKeyword:User.Identity.Name);
            return View(data);
        }
    }
}
