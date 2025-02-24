using AutoMapper;
using Domain.Dtos;
using Domain.Entites;

namespace Infrastructure.Profiles;

public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Comment, CommentGetDto>();

            CreateMap<CommentCreateDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));

            CreateMap<News, NewsGetDto>();

            CreateMap<NewsCreateDto, News>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

            CreateMap<NewsUpdateDto, News>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));

            CreateMap<Tag, TagCreateDto>();

            CreateMap<TagCreateDto, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

            CreateMap<User, UserGetDto>();

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Nickname));

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Nickname));
            CreateMap<Video, VideoGetDto>();

            CreateMap<VideoCreateDto, Video>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());

            CreateMap<VideoUpdateDto, Video>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow.AddHours(5)));

            CreateMap<UploadedFile, UploadedFileGetDto>();

            CreateMap<UploadedFileCreateDto, UploadedFile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
        }
    }