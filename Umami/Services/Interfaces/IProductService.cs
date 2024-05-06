using Umami.Models.Domain;

namespace Umami.Services.Interfaces
{
    public interface IProductService
    {
        bool Add(Product model);
        bool Update(Product model);
        Product GetById(int id);
        bool Delete(int id);
        IQueryable<Product> List();
        IQueryable<Product> GetProductsForRecipe(int recipeId);
    }
}
