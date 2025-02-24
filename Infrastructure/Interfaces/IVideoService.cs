using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IVideoService
{
    Task<Response<List<VideoGetDto>>> GetAllAsync (VideoFilter video);
    Task<Response<VideoGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(VideoCreateDto video);
    Task<Response<string>> DeleteAsync(int id);
}