using Umami.Models.Domain;
using Umami.Models.ViewModels;

namespace Umami.Services.Interfaces
{
    public interface IRecipeService
    {
        bool Add(Recipe model);
        bool Update(Recipe model);
        Recipe GetById(int id);
        bool Delete(int id);
        IQueryable<Recipe> List(string keyword);
        IQueryable<Recipe> GetRecipesForList();
        Recipe MapVMtoRecipe(RecipeEditorVM vm);
        RecipeEditorVM MapRecipeToVM(Recipe recipe);
        CookingRecipeVM GetCookRecipeVMbyId(int recipeId);
    }
}
