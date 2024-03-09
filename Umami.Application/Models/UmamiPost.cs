using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Umami.Application.Models
{
    public class UmamiPost : IDatabaseModel
    {
        public virtual int Id { get; set; }
        public virtual string? Title { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? PicturePath { get; set; }
        public virtual int? RecipeId { get; set; }

        protected UmamiPost()
        {

        }

        protected UmamiPost(string title, string description, string picturePath, int? recipeId)
        {
            Title = title;
            Description = description;
            PicturePath = picturePath;
            RecipeId = recipeId is null ? -1 : recipeId;
        }

        public static UmamiPost Create(string title, string description, string picturePath, int? recipeId)
        {
            return new UmamiPost(title, description, picturePath, recipeId);
        }

        public void Update(string title, string description, string picturePath, int? recipeId)
        {
            if (title == null)
            {
                throw new ArgumentNullException(
                    nameof(title),
                    "Title can't be null");
            }

            Title = title;
            Description = description;
            PicturePath = picturePath;
            RecipeId = recipeId is null ? -1 : recipeId;
        }
    }
}
