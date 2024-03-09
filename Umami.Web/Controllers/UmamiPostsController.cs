using Microsoft.AspNetCore.Mvc;
using Umami.Application.Services.DTO.UmamiPostDto;
using Umami.Application.Services.Interfaces.Services;

namespace Umami.Web.Controllers
{
    public class UmamiPostsController : Controller
    {

        private readonly IUmamiPostService _umamiPostService;

        public UmamiPostsController(IUmamiPostService umamiPostService)
        {
            _umamiPostService = umamiPostService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUmamiPostAsync(
            CreateUmamiPostDto dto)
        {
            var umamiPostId = await _umamiPostService.CreateUmamiPostAsync(dto);
            return Ok(umamiPostId);
        }

        [HttpDelete("delete/{umamiPostId}")]
        public async Task<IActionResult> DeleteUmamiPostAsync(
            int? umamiPostId)
        {
            await _umamiPostService.DeleteUmamiPostAsync(umamiPostId);
            return Ok(umamiPostId);
        }

        [HttpPatch("update/{umamiPostId}")]
        public async Task<IActionResult> UpdateUmamiPostAsync(
            int? umamiPostId,
            UpdateUmamiPostDto dto)
        {
            await _umamiPostService.UpdateUmamiPostAsync(
                umamiPostId,
                dto);

            return Ok(umamiPostId);
        }

        [HttpGet("detail/{umamiPostId}")]
        public async Task<IActionResult> GetUmamiPostAsync(
            int? umamiPostid)
        {
            return Ok();
        }
    }
}
