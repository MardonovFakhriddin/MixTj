using Domain.Dtos;
using Domain.Filters;
using Infrastructure.Responses;

namespace Infrastructure.Interfaces;

public interface IUploadedFileService
{
    Task<Response<List<UploadedFileGetDto>>> GetAllAsync (UploadedFileFilter file);
    Task<Response<UploadedFileGetDto>> GetByIdAsync(int id);
    Task<Response<string>> CreateAsync(UploadedFileCreateDto file);
    Task<Response<string>> DeleteAsync(int id);
}