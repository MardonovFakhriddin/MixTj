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

public class TagService(DataContext _context, IMapper _mapper) : ITagService
{

    public async Task<Response<List<TagGetDto>>> GetAllAsync(TagFilter filter)
    {
        IQueryable<Tag> tagsQuery = _context.Tags;

        if (!string.IsNullOrEmpty(filter.Name))
            tagsQuery = tagsQuery.Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()));

        var tagList = await tagsQuery.ToListAsync();
        var result = _mapper.Map<List<TagGetDto>>(tagList);
        return new Response<List<TagGetDto>>(result);
    }

    public async Task<Response<TagGetDto>> GetByIdAsync(int id)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null)
            return new Response<TagGetDto>(HttpStatusCode.NotFound, "Tag Not Found");

        var tagDto = _mapper.Map<TagGetDto>(tag);
        return new Response<TagGetDto>(tagDto);
    }

    public async Task<Response<string>> CreateAsync(TagCreateDto tagDto)
    {
        var tag = _mapper.Map<Tag>(tagDto);
        await _context.Tags.AddAsync(tag);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to create tag")
            : new Response<string>(HttpStatusCode.Created, "Tag Created");
    }

    public async Task<Response<string>> UpdateAsync(TagUpdateDto tagDto)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == tagDto.Id);

        if (tag == null)
            return new Response<string>(HttpStatusCode.NotFound, "Tag Not Found");

        _mapper.Map(tagDto, tag);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to update tag")
            : new Response<string>(HttpStatusCode.OK, "Tag Updated");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag == null)
            return new Response<string>(HttpStatusCode.NotFound, "Tag Not Found");

        _context.Tags.Remove(tag);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete tag")
            : new Response<string>(HttpStatusCode.OK, "Tag Deleted");
    }
}
