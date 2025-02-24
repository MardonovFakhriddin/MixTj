using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<UserGetDto>>> GetAllAsync (UserFilter user);
    Task<Response<UserGetDto>> GetByIdAsync(string id);
    Task<Response<string>> CreateAsync(UserCreateDto user);
    Task<Response<string>> UpdateAsync(UserUpdateDto user);
    Task<Response<string>> DeleteAsync(string id);
}