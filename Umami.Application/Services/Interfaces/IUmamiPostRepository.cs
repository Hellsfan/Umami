using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umami.Application.Models;

namespace Umami.Application.Services.Interfaces
{
    public interface IUmamiPostRepository : IRepository<UmamiPost>
    {
        Task<IEnumerable<UmamiPost>> GetRandomPostsForFeed(int amount); 
    }
}
