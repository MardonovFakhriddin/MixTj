using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MixTj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController(ITagService _tagService, ILogger<TagController> _logger) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<TagGetDto>>> GetAll([FromQuery] TagFilter filter)
    {
        _logger.LogInformation("Fetching all tags with filter {@Filter}", filter);
        return await _tagService.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<TagGetDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching tag with ID {Id}", id);
        return await _tagService.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Create(TagCreateDto dto)
    {
        _logger.LogInformation("Creating a new tag");
        return await _tagService.CreateAsync(dto);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Update(TagUpdateDto dto)
    {
        _logger.LogInformation("Updating tag with ID {Id}", dto.Id);
        return await _tagService.UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Delete(int id)
    {
        _logger.LogInformation("Deleting tag with ID {Id}", id);
        return await _tagService.DeleteAsync(id);
    }
}


