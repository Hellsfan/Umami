using Umami.Models.Domain;
using Umami.Services.Interfaces;

namespace Umami.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext context;
        public ProductService(DatabaseContext _context)
        {
            context = _context;
        }

        public bool Add(Product model)
        {
            try
            {
                context.Product.Add(model);
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

                var productRecipeList = context.recipeProduct.Where(rp => rp.ProductId == id);

                foreach (var item in productRecipeList)
                {
                    context.recipeProduct.Remove(item);
                }

                context.Product.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product GetById(int id)
        {
            return context.Product.Find(id);
        }

        public IQueryable<Product> GetProductsForRecipe(int recipeId)
        {
            var productIdList = context.recipeProduct.Where(r=>r.RecipeId == recipeId).Select(r=>r.ProductId).ToArray();
            var productList = context.Product.Where(p => productIdList.Contains(p.Id));
            return productList;
        }

        public IQueryable<Product> List()
        {
            var data = context.Product.AsQueryable();
            return data;
        }

        public bool Update(Product model)
        {
            try
            {
                context.Product.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
