using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MixTj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController(ICommentService commentService, ILogger<CommentController> logger)
{
    [HttpGet]
    public async Task<Response<List<CommentGetDto>>> GetAll([FromQuery] CommentFilter filter)
    {
        logger.LogInformation("Fetching all comments with filter {@Filter}", filter);
        return await commentService.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    public async Task<Response<CommentGetDto>> GetById(int id)
    {
        logger.LogInformation("Fetching comment with ID {Id}", id);
        return await commentService.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize]
    public async Task<Response<string>> Create(CommentCreateDto dto)
    {
        logger.LogInformation("Creating a new comment");
        return await commentService.CreateAsync(dto);
    }

    [HttpPut]
    [Authorize]
    public async Task<Response<string>> Update(CommentUpdateDto dto)
    {
        logger.LogInformation("Updating comment with ID {Id}", dto.Id);
        return await commentService.UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<Response<string>> Delete(int id)
    {
        logger.LogInformation("Deleting comment with ID {Id}", id);
        return await commentService.DeleteAsync(id);
    }
}
