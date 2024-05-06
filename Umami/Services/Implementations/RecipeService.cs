using Umami.Models.Domain;
using Umami.Models.ViewModels;
using Umami.Services.Interfaces;

namespace Umami.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly DatabaseContext context;
        private readonly IProductService productService;
        public RecipeService(DatabaseContext _context, IProductService _productService)
        {
            context = _context;
            productService = _productService;
        }
        public bool Add(Recipe model)
        {
            var entity = context.Recipe.Add(model).Entity;
            context.SaveChanges();

            try
            {
                foreach (var product in model.Products)
                {
                    var productRecipe = new RecipeProduct()
                    {
                        RecipeId = model.Id,
                        ProductId = model.Id
                    };

                    context.recipeProduct.Add(productRecipe);
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

                foreach (var product in data.Products)
                {
                    var recipeProduct = context.recipeProduct.Where(rp=>rp.ProductId==product&&rp.RecipeId==id).FirstOrDefault();
                    if (recipeProduct != null)
                    {
                        context.recipeProduct.Remove(recipeProduct);
                    }
                }

                var blogPostRecipe = context.BlogPostRecipe.Find(id);
                if(blogPostRecipe != null)
                {
                    var post = context.BlogPost.Find(blogPostRecipe.BlogPostId);
                    post.RecipeId = -1;
                    context.BlogPostRecipe.Remove(blogPostRecipe);
                }

                context.Recipe.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Recipe GetById(int id)
        {
            return context.Recipe.Find(id);
        }

        public IQueryable<Recipe> GetRecipesForList()
        {
            var data = context.Recipe.ToList();
            return data.AsQueryable();
        }

        public IQueryable<Recipe> List(string keyword)
        {
            var data = context.Recipe.Where(r=>r.CreatedByUser.Equals(keyword));
            return data;
        }

        public RecipeEditorVM MapRecipeToVM(Recipe recipe)
        {
            var blogPostVM = new RecipeEditorVM
            {
                Name = recipe.Name,
                Instructions = recipe.Instructions,
                Description = recipe.Description,
                RecipeImage = recipe.RecipeImage,
                Products = recipe.Products,
                CreatedByUser = recipe.CreatedByUser,
                AmountsInput = String.Join(',', recipe.ProductAmounts)
            };

            return blogPostVM;
        }

        public Recipe MapVMtoRecipe(RecipeEditorVM vm)
        {
            var recipe = new Recipe
            {
                Id = vm.Id,
                Name = vm.Name,
                Instructions = vm.Instructions,
                Description = vm.Description,
                RecipeImage = vm.RecipeImage,
                Products = vm.Products,
                CreatedByUser = vm.CreatedByUser,
                ProductAmounts = vm.AmountsInput.Split(',').ToList<string>()
            };

            return recipe;
        }

        public bool Update(Recipe model)
        {
            try
            {
                var ProductRecipesToBeDeleted = context.recipeProduct.Where(a => a.RecipeId == model.Id && !model.Products.Contains(a.ProductId)).ToList();
                foreach (var recipeProduct in ProductRecipesToBeDeleted)
                {
                    context.recipeProduct.Remove(recipeProduct);
                }

                foreach (int productId in model.Products)
                {
                    var recipeProduct = context.recipeProduct.FirstOrDefault(a => a.RecipeId == model.Id && a.ProductId == productId);
                    if (recipeProduct == null)
                    {
                        recipeProduct = new RecipeProduct { ProductId = productId, RecipeId = model.Id };
                        context.recipeProduct.Add(recipeProduct);
                    }
                }

                context.Recipe.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CookingRecipeVM GetCookRecipeVMbyId(int recipeId)
        {
            var recipe = this.GetById(recipeId);
            var recipeProdcutList = productService.GetProductsForRecipe(recipeId);
            List<string> alergensList = new List<string>();
            foreach (var product in recipeProdcutList)
            {
                if(!alergensList.Contains(product.Alergen)) alergensList.Add(product.Alergen);
            }
            var RecipeVM = new CookingRecipeVM()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                CreatedByUser = recipe.CreatedByUser,
                ProductAmounts = recipe.ProductAmounts,
                ListOfProducts = recipeProdcutList.ToList(),
                RecipeImage = recipe.RecipeImage
            };

            RecipeVM.alergensToString = String.Join(',', alergensList);

            return RecipeVM;
        }
    }
}
