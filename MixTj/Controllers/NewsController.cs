using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MixTj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController(INewsService _newsService, ILogger<NewsController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<NewsGetDto>>> GetAll([FromQuery] NewsFilter filter)
    {
        _logger.LogInformation("Fetching all news with filter {@Filter}", filter);
        return await _newsService.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<NewsGetDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching news with ID {Id}", id);
        return await _newsService.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<Response<string>> Create(NewsCreateDto dto)
    {
        _logger.LogInformation("Creating a new news article");
        return await _newsService.CreateAsync(dto);
    }

    [HttpPut]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<Response<string>> Update(NewsUpdateDto dto)
    {
        _logger.LogInformation("Updating news article with ID {Id}", dto.Id);
        return await _newsService.UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Delete(int id)
    {
        _logger.LogInformation("Deleting news article with ID {Id}", id);
        return await _newsService.DeleteAsync(id);
    }
}


