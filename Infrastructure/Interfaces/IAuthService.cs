using Domain.Dtos.Auth;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IAuthService
{
    Task<Response<string>> Register(RegisterDto model);
    Task<Response<string>> Login(LoginDto login);
    Task<Response<string>> RemoveRoleFromUser(RoleDto userRole);
    Task<Response<string>> AddRoleToUser(RoleDto userRole);
}