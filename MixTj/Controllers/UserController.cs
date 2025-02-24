using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MixTj.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, ILogger<UserController> logger)
{
    [HttpGet]
    public async Task<Response<List<UserGetDto>>> GetAll([FromQuery] UserFilter filter)
    {
        logger.LogInformation("Fetching all users with filter {@Filter}", filter);
        return await userService.GetAllAsync(filter);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<Response<UserGetDto>> GetById(string id)
    {
        logger.LogInformation("Fetching user with ID {Id}", id);
        return await userService.GetByIdAsync(id);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Create(UserCreateDto dto)
    {
        logger.LogInformation("Creating a new user");
        return await userService.CreateAsync(dto);
    }

    [HttpPut]
    [Authorize]
    public async Task<Response<string>> Update(UserUpdateDto dto)
    {
        logger.LogInformation("Updating user with ID {Id}", dto.Id);
        return await userService.UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> Delete(string id)
    {
        logger.LogInformation("Deleting user with ID {Id}", id);
        return await userService.DeleteAsync(id);
    }
}

