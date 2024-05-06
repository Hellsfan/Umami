using System.ComponentModel.DataAnnotations.Schema;

namespace Umami.Models.Domain
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> ProductAmounts { get; set; }
        public string Instructions { get; set; }
        public string CreatedByUser { get; set; }
        public List<int> Products { get; set; }
        public string? RecipeImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
