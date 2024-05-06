using Umami.Models.Domain;

namespace Umami.Services.Interfaces
{
    public interface IReviewService
    {
        public bool Add(Review review);
        public List<Review> GetReviewsForRecipe(int recipeId);
        public double GetStarsForRecipe(int recipeId);
        public bool deleteReviewsForRecipe(int recipeId);
    }
}
