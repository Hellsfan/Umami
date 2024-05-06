using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Umami.Models;
using Umami.Models.ViewModels;
using Umami.Services.Interfaces;

namespace Umami.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostService blogPostService;
        private readonly IReviewService reviewService;

        public HomeController(ILogger<HomeController> logger, IBlogPostService _blogPostService, IReviewService _reviewService)
        {
            _logger = logger;
            blogPostService= _blogPostService;
            reviewService= _reviewService;
        }

        public IActionResult Index(string searchKeyword = "", int currentPage = 1)
        {
            var Posts = blogPostService.List(searchKeyword, true, currentPage);
            return View(Posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult BlogPostDetail(int blogPostId)
        {
            var post = blogPostService.GetById(blogPostId);
            var postVM = new BlogPostDetailVM()
            {
                Title = post.Title,
                Description = post.Description,
                Text = post.Text,
                CreatedByUser = post.CreatedByUser,
                TypeOfPost = post.TypeOfPost,
                RecipeId = post.RecipeId,
                PostImage = post.PostImage,
                RecipeStars = 0
            };

            if (post.RecipeId != -1)
            {
                var recipeStars = reviewService.GetStarsForRecipe((int)post.RecipeId);
                var reviews = reviewService.GetReviewsForRecipe((int)post.RecipeId);
                postVM.RecipeStars = recipeStars;
                postVM.Reviews= reviews;
            }
            return View(postVM);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
