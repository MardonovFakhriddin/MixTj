using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface INewsService
{
    Task<Response<List<NewsGetDto>>> GetAllAsync (NewsFilter news);
    Task<Response<NewsGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(NewsCreateDto news);
    Task<Response<string>> UpdateAsync(NewsUpdateDto news);
    Task<Response<string>> DeleteAsync(int id);
}