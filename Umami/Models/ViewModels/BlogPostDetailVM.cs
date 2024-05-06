using System.ComponentModel.DataAnnotations.Schema;
using Umami.Models.Domain;

namespace Umami.Models.ViewModels
{
    public class BlogPostDetailVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string TypeOfPost { get; set; }
        public int? RecipeId { get; set; }
        public string? PostImage { get; set; }
        public string CreatedByUser { get; set; }
        public  double RecipeStars {  get; set; }
        public List<Review> Reviews { get; set; }
    }
}
