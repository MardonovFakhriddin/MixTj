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

public class UploadedFileService(DataContext _context, IMapper _mapper) : IUploadedFileService
{

    public async Task<Response<List<UploadedFileGetDto>>> GetAllAsync(UploadedFileFilter filter)
    {
        IQueryable<UploadedFile> filesQuery = _context.Files;

        // Фильтрация по имени файла
        if (!string.IsNullOrEmpty(filter.FileName))
            filesQuery = filesQuery.Where(f => f.FileName.ToLower().Contains(filter.FileName.ToLower()));

        // Фильтрация по пути файла
        if (!string.IsNullOrEmpty(filter.FilePath))
            filesQuery = filesQuery.Where(f => f.FilePath.ToLower().Contains(filter.FilePath.ToLower()));

        var fileList = await filesQuery.ToListAsync();
        var result = _mapper.Map<List<UploadedFileGetDto>>(fileList);
        return new Response<List<UploadedFileGetDto>>(result);
    }

    public async Task<Response<UploadedFileGetDto>> GetByIdAsync(int id)
    {
        var file = await _context.Files
            .FirstOrDefaultAsync(f => f.Id == id);

        if (file == null)
            return new Response<UploadedFileGetDto>(HttpStatusCode.NotFound, "File Not Found");

        var fileDto = _mapper.Map<UploadedFileGetDto>(file);
        return new Response<UploadedFileGetDto>(fileDto);
    }

    public async Task<Response<string>> CreateAsync(UploadedFileCreateDto fileDto)
    {
        var file = _mapper.Map<UploadedFile>(fileDto);
        await _context.Files.AddAsync(file);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to upload file")
            : new Response<string>(HttpStatusCode.Created, "File Uploaded");
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var file = await _context.Files
            .FirstOrDefaultAsync(f => f.Id == id);

        if (file == null)
            return new Response<string>(HttpStatusCode.NotFound, "File Not Found");

        _context.Files.Remove(file);
        var result = await _context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Failed to delete file")
            : new Response<string>(HttpStatusCode.OK, "File Deleted");
    }
}
