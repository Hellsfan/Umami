using System.Linq;
using Umami.Models.Domain;
using Umami.Models.ViewModels;
using Umami.Services.Interfaces;

namespace Umami.Services.Implementations
{
    public class BlogPostService : IBlogPostService
    {
        private readonly DatabaseContext context;
        public BlogPostService(DatabaseContext _context)
        {
            context = _context;
        }

        public bool Add(BlogPost model)
        {
            try
            {
                context.BlogPost.Add(model);
                context.SaveChanges();

                if(model.RecipeId != -1)
                {
                    var blogPostRecipe = new BlogPostRecipe
                    {
                        BlogPostId = model.Id,
                        RecipeId = (int)model.RecipeId
                    };
                    context.BlogPostRecipe.Add(blogPostRecipe);
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);

                if (data == null) return false;

                if(data.RecipeId != -1)
                {
                    var blogPostRecipeObj = context.BlogPostRecipe.Where(bpr => bpr.BlogPostId == data.Id).FirstOrDefault();
                    if (blogPostRecipeObj != null)
                    {
                        context.BlogPostRecipe.Remove(blogPostRecipeObj);
                    }
                }

                context.BlogPost.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BlogPost GetById(int id)
        {
            return context.BlogPost.Find(id);
        }

        public Recipe GetRecipeByBlogPostId(int blogPostId)
        {
            var blogPostRecipe = context.BlogPostRecipe.Where(bpr => bpr.BlogPostId == blogPostId).FirstOrDefault();
            var recipe = context.Recipe.Where(r => r.Id == blogPostRecipe.RecipeId).FirstOrDefault();
            return recipe;
        }

        public BlogPostVM List(string searchKeyword = "", bool paging = false, int currentPage = 0)
        {
            var data = new BlogPostVM();

            var list = context.BlogPost.ToList();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                searchKeyword = searchKeyword.ToLower();
                list = list
                    .Where(a => a.Title.ToLower().Contains(searchKeyword)
                    || a.TypeOfPost.ToLower().Contains(searchKeyword)
                    || a.CreatedByUser.ToLower().Contains(searchKeyword)
                    ||a.Description.ToLower().Contains(searchKeyword))
                    .ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }


            data.BlogPostList = list.AsQueryable();
            return data;
        }

        public bool Update(BlogPost model)
        {
            try
            {
                if (model.RecipeId != -1)
                {
                    var currentBlogPostRecipe = context.BlogPostRecipe.Where(bpr => bpr.BlogPostId == model.Id).FirstOrDefault();
                    if (currentBlogPostRecipe != null)
                    {
                        context.Remove(currentBlogPostRecipe);
                    }

                    var blogPostRecipe = new BlogPostRecipe
                    {
                        BlogPostId = model.Id,
                        RecipeId = (int)model.RecipeId
                    };
                    context.Add(blogPostRecipe);
                }

                context.BlogPost.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BlogPost MapVMtoBlogPost(BlogPostEditorVM vm)
        {
            var blogPost = new BlogPost
            {
                Id = vm.Id,
                Title = vm.Title,
                Text = vm.Text,
                Description = vm.Description,
                TypeOfPost = vm.TypeOfPost,
                PostImage = vm.PostImage,
                RecipeId = vm.RecipeId,
                CreatedByUser = vm.CreatedByUser
            };

            return blogPost;
        }

        public BlogPostEditorVM MapBlogPosttoVM(BlogPost bp)
        {
            var blogPostVM = new BlogPostEditorVM
            {
                Title = bp.Title,
                Text = bp.Text,
                Description = bp.Description,
                TypeOfPost = bp.TypeOfPost,
                PostImage = bp.PostImage,
                RecipeId = bp.RecipeId,
                CreatedByUser = bp.CreatedByUser
            };

            return blogPostVM;
        }
    }
}
