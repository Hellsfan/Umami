using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umami.Application.Models;
using Umami.Application.Services.DTO.UmamiPostDto;
using Umami.Application.Services.Interfaces;
using Umami.Application.Services.Interfaces.Services;

namespace Umami.Application.Services.Implementations
{
    public class UmamiPostService : IUmamiPostService
    {
        private readonly IMapper _mapper;
        private readonly IUmamiPostRepository _umamiPostRepository;

        public UmamiPostService(IMapper mapper, IUmamiPostRepository umamiPostRepository)
        {
            _mapper = mapper;
            _umamiPostRepository = umamiPostRepository;
        }

        public async Task<long?> CreateUmamiPostAsync(CreateUmamiPostDto dto)
        {
            var umamiPost = UmamiPost.Create(
                dto.Title,
                dto.Description,
                dto.PicturePath,
                dto.RecipeId
                );

            var umamiPostId = await _umamiPostRepository.SaveAsync(umamiPost );

            return umamiPostId;
        }

        public async Task DeleteUmamiPostAsync(int? UmamiPostId)
        {
            if (UmamiPostId is null)
            {
                throw new ArgumentNullException(
                    nameof(UmamiPostId),
                    "UmamiPost id is required");
            }

            var umamiPost = await _umamiPostRepository.GetAsync(
                UmamiPostId);

            if (umamiPost is null)
            {
                throw new ArgumentException(
                    $"UmamiPost with id {UmamiPostId} does not exist",
                    nameof(umamiPost));
            }

            await _umamiPostRepository.DeleteAsync(umamiPost);
        }

        public async Task UpdateUmamiPostAsync(int? umamiPostId, UpdateUmamiPostDto dto)
        {
            if (umamiPostId is null)
            {
                throw new ArgumentNullException(
                    nameof(umamiPostId),
                    "Product id is requred");
            }

            var umamiPost = await _umamiPostRepository.GetAsync(
                umamiPostId);

            if (umamiPost is null)
            {
                throw new ArgumentException(
                    $"Product wit id {umamiPostId} does not exist",
                    nameof(umamiPost));
            }

            umamiPost.Update(
                dto.Title,
                dto.Description,
                dto.PicturePath,
                dto.RecipeId);

            await _umamiPostRepository.UpdateAsync(umamiPost);
        }
    }
}
