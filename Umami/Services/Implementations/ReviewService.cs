using Umami.Models.Domain;
using Umami.Services.Interfaces;

namespace Umami.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly DatabaseContext context;

        public ReviewService(DatabaseContext _context)
        {
            context = _context;
        }
        public bool Add(Review review)
        {
            try
            {
                context.Review.Add(review);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool deleteReviewsForRecipe(int recipeId)
        {
            try
            {
                var listOfReview = context.Review.Where(r => r.RecipeId == recipeId);
                foreach (var review in listOfReview)
                {
                    context.Remove(review);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Review> GetReviewsForRecipe(int recipeId)
        {
            var listOfReview = context.Review.Where(r => r.RecipeId == recipeId);
            return listOfReview.ToList();
        }

        public double GetStarsForRecipe(int recipeId)
        {
            if (context.Review.Where(r => r.RecipeId == recipeId).Any())
            {
                var stars = context.Review.Where(r => r.RecipeId == recipeId).Select(r => r.Stars);
                return Math.Truncate(stars.Average() * 100) / 100;
            }
            else
            {
                return -1;
            }
        }
    }
}
