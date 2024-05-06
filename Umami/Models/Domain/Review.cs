namespace Umami.Models.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string Text { get; set; }
        public int RecipeId { get; set; }
        public string CreatedByUser { get; set; }
    }
}
