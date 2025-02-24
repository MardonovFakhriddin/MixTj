using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entites;
using Domain.Filters;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext _context, IMapper _mapper) : IUserService
{
    public async Task<Response<List<UserGetDto>>> GetAllAsync(UserFilter filter)
    {
        IQueryable<User> users = _context.Users;

        if (!string.IsNullOrEmpty(filter.Email))
            users = users.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));

        if (!string.IsNullOrEmpty(filter.Nickname))
            users = users.Where(u => u.UserName.ToLower().Contains(filter.Nickname.ToLower()));

        if (!string.IsNullOrEmpty(filter.Profile))
            users = users.Where(u => u.Profile.ToLower().Contains(filter.Profile.ToLower()));

        var userList = await users.ToListAsync();
        var result = _mapper.Map<List<UserGetDto>>(userList);
        return new Response<List<UserGetDto>>(result);
    }

    public async Task<Response<UserGetDto>> GetByIdAsync(string id)
    {

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return new Response<UserGetDto>(HttpStatusCode.NotFound, "User Not Found");

        var userDto = _mapper.Map<UserGetDto>(user);
        return new Response<UserGetDto>(userDto);
    }

    public async Task<Response<string>> CreateAsync(UserCreateDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        await _context.Users.AddAsync(user);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to create user")
            : new Response<string>(HttpStatusCode.Created, "User Created");
    }

    public async Task<Response<string>> UpdateAsync(UserUpdateDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
        if (user == null)
            return new Response<string>(HttpStatusCode.NotFound, "User Not Found");

        _mapper.Map(userDto, user);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to update user")
            : new Response<string>(HttpStatusCode.OK, "User Updated");
    }

    public async Task<Response<string>> DeleteAsync(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return new Response<string>(HttpStatusCode.NotFound, "User Not Found");

        _context.Users.Remove(user);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete user")
            : new Response<string>(HttpStatusCode.OK, "User Deleted");
    }
}
