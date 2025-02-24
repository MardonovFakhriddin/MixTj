using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MixTj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController(IVideoService _videoService, ILogger<VideoController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<VideoGetDto>>> GetAll([FromQuery] VideoFilter filter)
    {
        _logger.LogInformation("Fetching all videos with filter {@Filter}", filter);
        return await _videoService.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<VideoGetDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching video with ID {Id}", id);
        return await _videoService.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<Response<string>> Create(VideoCreateDto dto)
    {
        _logger.LogInformation("Creating a new video");
        return await _videoService.CreateAsync(dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Delete(int id)
    {
        _logger.LogInformation("Deleting video with ID {Id}", id);
        return await _videoService.DeleteAsync(id);
    }
}
