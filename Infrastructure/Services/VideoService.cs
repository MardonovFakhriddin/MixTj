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

public class VideoService : IVideoService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public VideoService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<VideoGetDto>>> GetAllAsync(VideoFilter filter)
    {
        IQueryable<Video> videos = _context.Videos;

        if (!string.IsNullOrEmpty(filter.Title))
            videos = videos.Where(v => v.Title.ToLower().Contains(filter.Title.ToLower()));

        if (!string.IsNullOrEmpty(filter.Description))
            videos = videos.Where(v => v.Description.ToLower().Contains(filter.Description.ToLower()));

        if (!string.IsNullOrEmpty(filter.URL))
            videos = videos.Where(v => v.URL.ToLower().Contains(filter.URL.ToLower()));

        if (!string.IsNullOrEmpty(filter.VideoType))
            videos = videos.Where(v => v.VideoType.ToLower().Contains(filter.VideoType.ToLower()));

        var videoList = await videos.ToListAsync();
        var result = _mapper.Map<List<VideoGetDto>>(videoList);
        return new Response<List<VideoGetDto>>(result);
    }

    public async Task<Response<VideoGetDto>> GetByIdAsync(int id)
    {
        var video = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);
        if (video == null)
            return new Response<VideoGetDto>(HttpStatusCode.NotFound, "Video Not Found");

        var videoDto = _mapper.Map<VideoGetDto>(video);
        return new Response<VideoGetDto>(videoDto);
    }

    public async Task<Response<string>> CreateAsync(VideoCreateDto videoDto)
    {
        var video = _mapper.Map<Video>(videoDto);
        await _context.Videos.AddAsync(video);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to create video")
            : new Response<string>(HttpStatusCode.Created, "Video Created");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var video = await _context.Videos.FirstOrDefaultAsync(v => v.Id == id);
        if (video == null)
            return new Response<string>(HttpStatusCode.NotFound, "Video Not Found");

        _context.Videos.Remove(video);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete video")
            : new Response<string>(HttpStatusCode.OK, "Video Deleted");
    }
}
