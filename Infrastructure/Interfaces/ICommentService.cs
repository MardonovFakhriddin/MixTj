using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface ICommentService
{
    Task<Response<List<CommentGetDto>>> GetAllAsync (CommentFilter comment);
    Task<Response<CommentGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(CommentCreateDto comment);
    Task<Response<string>> UpdateAsync(CommentUpdateDto comment);
    Task<Response<string>> DeleteAsync(int id);
}