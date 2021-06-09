using AutoMapper;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;

namespace BasicAPI.Services.WebAPI.Profiles
{
    public class BasicAPIProfeiles : Profile
    {
        public BasicAPIProfeiles()
        {
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<BookDTO, Book>().ReverseMap();
            CreateMap<BookCreatedDTO, Book>().ReverseMap();
        }
    }
}
