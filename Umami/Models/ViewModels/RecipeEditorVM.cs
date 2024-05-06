using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Umami.Models.ViewModels
{
    public class RecipeEditorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string? CreatedByUser { get; set; }
        public string AmountsInput { get; set; }
        public List<int> Products { get; set; }
        public string? RecipeImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public IEnumerable<SelectListItem>? ProductList { get; set; }
        public MultiSelectList? MultiProductList { get; set; }
    }
}
