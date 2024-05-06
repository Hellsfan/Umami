using Umami.Models.Domain;

namespace Umami.Models.ViewModels
{
    public class BlogPostVM
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? searchKeyword { get; set; }

        public IQueryable<BlogPost> BlogPostList { get; set; }
    }
}
