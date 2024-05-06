using System.ComponentModel.DataAnnotations.Schema;

namespace Umami.Models.Domain
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string TypeOfPost { get; set; }
        public int? RecipeId { get; set; }
        public string? PostImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string CreatedByUser { get; set; }
    }
}
