namespace Umami.Models.Domain
{
    public class BlogPostRecipe
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public int RecipeId { get; set; }
    }
}
