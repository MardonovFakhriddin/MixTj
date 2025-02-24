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

public class NewsService(DataContext _context, IMapper _mapper) : INewsService
{

    public async Task<Response<List<NewsGetDto>>> GetAllAsync(NewsFilter filter)
    {
        IQueryable<News> newsQuery = _context.News;

        if (!string.IsNullOrEmpty(filter.Title))
            newsQuery = newsQuery.Where(n => n.Title.ToLower().Contains(filter.Title.ToLower()));

        if (!string.IsNullOrEmpty(filter.Content))
            newsQuery = newsQuery.Where(n => n.Content.ToLower().Contains(filter.Content.ToLower()));

        if (!string.IsNullOrEmpty(filter.Category))
            newsQuery = newsQuery.Where(n => n.Category.ToLower().Contains(filter.Category.ToLower()));

        var newsList = await newsQuery.ToListAsync();
        var result = _mapper.Map<List<NewsGetDto>>(newsList);
        return new Response<List<NewsGetDto>>(result);
    }

    public async Task<Response<NewsGetDto>> GetByIdAsync(int id)
    {
        var news = await _context.News
            .FirstOrDefaultAsync(n => n.Id == id);

        if (news == null)
            return new Response<NewsGetDto>(HttpStatusCode.NotFound, "News Not Found");

        var newsDto = _mapper.Map<NewsGetDto>(news);
        return new Response<NewsGetDto>(newsDto);
    }

    public async Task<Response<string>> CreateAsync(NewsCreateDto newsDto)
    {
        var news = _mapper.Map<News>(newsDto);
        await _context.News.AddAsync(news);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to create news")
            : new Response<string>(HttpStatusCode.Created, "News Created");
    }

    public async Task<Response<string>> UpdateAsync(NewsUpdateDto newsDto)
    {
        var news = await _context.News
            .FirstOrDefaultAsync(n => n.Id == newsDto.Id);

        if (news == null)
            return new Response<string>(HttpStatusCode.NotFound, "News Not Found");

        _mapper.Map(newsDto, news);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to update news")
            : new Response<string>(HttpStatusCode.OK, "News Updated");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var news = await _context.News
            .FirstOrDefaultAsync(n => n.Id == id);

        if (news == null)
            return new Response<string>(HttpStatusCode.NotFound, "News Not Found");

        _context.News.Remove(news);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete news")
            : new Response<string>(HttpStatusCode.OK, "News Deleted");
    }
}
