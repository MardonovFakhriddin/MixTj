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

public class CommentService(DataContext _context, IMapper _mapper) : ICommentService
{

    public async Task<Response<List<CommentGetDto>>> GetAllAsync(CommentFilter filter)
    {
        IQueryable<Comment> comments = _context.Comments;

        if (!string.IsNullOrEmpty(filter.Text))
            comments = comments.Where(c => c.Text.ToLower().Contains(filter.Text.ToLower()));

        var commentList = await comments.ToListAsync();
        var result = _mapper.Map<List<CommentGetDto>>(commentList);
        return new Response<List<CommentGetDto>>(result);
    }

    public async Task<Response<CommentGetDto>> GetByIdAsync(int id)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
            return new Response<CommentGetDto>(HttpStatusCode.NotFound, "Comment Not Found");

        var commentDto = _mapper.Map<CommentGetDto>(comment);
        return new Response<CommentGetDto>(commentDto);
    }

    public async Task<Response<string>> CreateAsync(CommentCreateDto commentDto)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        await _context.Comments.AddAsync(comment);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to create comment")
            : new Response<string>(HttpStatusCode.Created, "Comment Created");
    }

    public async Task<Response<string>> UpdateAsync(CommentUpdateDto commentDto)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == commentDto.Id);

        if (comment == null)
            return new Response<string>(HttpStatusCode.NotFound, "Comment Not Found");

        _mapper.Map(commentDto, comment);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to update comment")
            : new Response<string>(HttpStatusCode.OK, "Comment Updated");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
            return new Response<string>(HttpStatusCode.NotFound, "Comment Not Found");

        _context.Comments.Remove(comment);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete comment")
            : new Response<string>(HttpStatusCode.OK, "Comment Deleted");
    }
}
