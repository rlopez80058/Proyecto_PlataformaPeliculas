using Microsoft.AspNetCore.Mvc;
using Movie.Api.DTOs;
using Movie.Api.Services;

namespace Movie.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _libraryService.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaveLibraryItemDto dto)
        {
            var result = await _libraryService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateLibraryItemDto dto)
        {
            await _libraryService.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpPut("{id}/favorite")]
        public async Task<IActionResult> ToggleFavorite(int id)
        {
            await _libraryService.ToggleFavoriteAsync(id);
            return Ok();
        }

        [HttpPut("{id}/review")]
        public async Task<IActionResult> AddReview(int id, [FromBody] UpdateLibraryItemDto dto)
        {
            await _libraryService.UpdateAsync(id, dto);
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _libraryService.DeleteAsync(id);
            return Ok();
        }
    }
}