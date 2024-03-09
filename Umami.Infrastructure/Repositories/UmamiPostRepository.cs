using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umami.Application.Models;
using Umami.Application.Services.Interfaces;
using Umami.Infrastructure.Database.Configuration;

namespace Umami.Infrastructure.Repositories
{
    public class UmamiPostRepository : IUmamiPostRepository
    {
        private readonly DBContext _dbContext;

        public UmamiPostRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task DeleteAsync(UmamiPost entity)
        {
            _dbContext.UmamiPosts.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<UmamiPost> GetAsync(int? id)
        {
            return await _dbContext.UmamiPosts.AsQueryable()
                .Where(e => e.Id == id)
                .SingleAsync();
        }

        public async Task<IEnumerable<UmamiPost>> GetAsync()
        {
            return await _dbContext.UmamiPosts.AsQueryable()
                .ToListAsync();
        }

        public Task<IEnumerable<UmamiPost>> GetRandomPostsForFeed(int amount)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> SaveAsync(UmamiPost entity)
        {
            var data = await _dbContext.UmamiPosts.AddAsync(entity);

            return entity.Id;
        }

        public Task UpdateAsync(UmamiPost entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }
    }
}
