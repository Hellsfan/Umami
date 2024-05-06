using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Umami.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<BlogPostRecipe> BlogPostRecipe { get; set; }
        public DbSet<RecipeProduct> recipeProduct { get; set; }
        public DbSet<Review> Review { get; set; }

    }
}

