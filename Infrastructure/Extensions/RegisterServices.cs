using Domain.Entites;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Seed;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class RegisterServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IUploadedFileService, UploadedFileService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<Seeder>();
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAuthService, AuthService>();
    }
}