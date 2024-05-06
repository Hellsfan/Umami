using Microsoft.AspNetCore.Mvc;
using Umami.Models.Domain;
using Umami.Services.Implementations;
using Umami.Services.Interfaces;

namespace Umami.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService _reviewService)
        {
            reviewService = _reviewService;
        }

        public IActionResult Add(int recipeId)
        {
            Review review = new Review { RecipeId = recipeId };
            return View(review);
        }

        [HttpPost]
        public IActionResult Add(Review model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = reviewService.Add(model);
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
    }
}
