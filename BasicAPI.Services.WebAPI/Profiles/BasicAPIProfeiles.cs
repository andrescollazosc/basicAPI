using AutoMapper;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;
using System.Collections.Generic;

namespace BasicAPI.Services.WebAPI.Profiles
{
    public class BasicAPIProfeiles : Profile
    {
        public BasicAPIProfeiles()
        {
            CreateMap<AuthorDTO, Author>().ReverseMap();
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();

            CreateMap<Book, BookDTO>().
                ForMember(bookDto => bookDto.Authors, options => options.MapFrom(MapBookDtoAuthors));

            CreateMap<Book, BookCreatedDTO>().ReverseMap()
                .ForMember(book => book.AuthorsBooks, options => options.MapFrom(MapAtuthorsBooks));

            CreateMap<CommentCreateDTO, Comment>().ReverseMap();
            CreateMap<CommentDto, Comment>().ReverseMap();
        }

        private List<AuthorDTO> MapBookDtoAuthors(Book book, BookDTO bookDTO)
        {
            List<AuthorDTO> result = new();

            if (book.AuthorsBooks == null)
            {
                return result;
            }

            foreach (var authorBook in book.AuthorsBooks)
            {
                result.Add(new() { 
                    Id = authorBook.AuthorId, 
                    Passport = authorBook.Author.Passport,
                    FirstName = authorBook.Author.FirstName,  
                    LasttName = authorBook.Author.LasttName
                });
            }

            return result;

        }

        private List<AuthorBook> MapAtuthorsBooks(BookCreatedDTO bookCreatedDTO, Book book)
        {
            List<AuthorBook> result = new();

            if (bookCreatedDTO.AuthorsId == null)
            {
                return result;
            }

            foreach (var authorId in bookCreatedDTO.AuthorsId)
            {
                result.Add(new() { AuthorId = authorId });
            }

            return result;
        }

    }
}
