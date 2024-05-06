namespace Umami.Models.Domain
{
    public class RecipeProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RecipeId { get; set; }
    }
}
