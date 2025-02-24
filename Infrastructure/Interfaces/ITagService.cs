using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface ITagService
{
    Task<Response<List<TagGetDto>>> GetAllAsync (TagFilter tag);
    Task<Response<TagGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(TagCreateDto tag);
    Task<Response<string>> UpdateAsync(TagUpdateDto tag);
    Task<Response<string>> DeleteAsync(int id);
}