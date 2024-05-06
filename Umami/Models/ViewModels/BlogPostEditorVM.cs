using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Umami.Models.ViewModels
{
    public class BlogPostEditorVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string TypeOfPost { get; set; }
        public int? RecipeId { get; set; }
        public string? PostImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        public string? CreatedByUser { get; set; }
        public IEnumerable<SelectListItem>? RecipeList { get; set; }
    }
}
