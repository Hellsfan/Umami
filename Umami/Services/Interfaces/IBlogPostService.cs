using Umami.Models.Domain;
using Umami.Models.ViewModels;

namespace Umami.Services.Interfaces
{
    public interface IBlogPostService
    {
        bool Add(BlogPost model);
        bool Update(BlogPost model);
        BlogPost GetById(int id);
        bool Delete(int id);
        BlogPostVM List(string searchKeyword = "", bool paging = false, int currentPage = 0);
        Recipe GetRecipeByBlogPostId(int blogPostId);
        BlogPost MapVMtoBlogPost(BlogPostEditorVM vm);
        BlogPostEditorVM MapBlogPosttoVM(BlogPost bp);
    }
}
