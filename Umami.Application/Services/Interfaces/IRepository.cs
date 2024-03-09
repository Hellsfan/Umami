using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umami.Application.Models;

namespace Umami.Application.Services.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IDatabaseModel
    {
        Task<TEntity> GetAsync(int? id);

        Task<IEnumerable<TEntity>> GetAsync();

        Task<int?> SaveAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
