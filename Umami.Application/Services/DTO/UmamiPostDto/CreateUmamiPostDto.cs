using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umami.Application.Services.DTO.UmamiPostDto
{
    public sealed class CreateUmamiPostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public int? RecipeId { get; set; }
    }
}
