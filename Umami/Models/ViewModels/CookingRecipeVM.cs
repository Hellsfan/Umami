using Umami.Models.Domain;

namespace Umami.Models.ViewModels
{
    public class CookingRecipeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string CreatedByUser { get; set; }
        public List<int> Products { get; set; }
        public string? RecipeImage { get; set; }
        public List<string> ProductAmounts { get; set; }
        public List<Product> ListOfProducts { get; set; }
        public string alergensToString {  get; set; }
    }
}
