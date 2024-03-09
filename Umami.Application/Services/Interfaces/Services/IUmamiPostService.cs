using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umami.Application.Services.DTO.UmamiPostDto;

namespace Umami.Application.Services.Interfaces.Services
{
    public interface IUmamiPostService
    {
        Task<long?> CreateUmamiPostAsync(
            CreateUmamiPostDto createProduct);

        Task UpdateUmamiPostAsync(
            int? productId,
            UpdateUmamiPostDto updateProduct);

        Task DeleteUmamiPostAsync(
           int? UmamiPostId);
    }
}
