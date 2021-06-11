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
            CreateMap<Author, AuthorDTO>();
            CreateMap<Author, AuthorDTOWithBooks>().
                ForMember(authorDto => authorDto.Books, options => options.MapFrom(MapAuthorDTOBooks));
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();

            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, BookDTOWithAuthors>().
                ForMember(bookDto => bookDto.Authors, options => options.MapFrom(MapBookDtoAuthors));

            CreateMap<Book, BookCreatedDTO>().ReverseMap()
                .ForMember(book => book.AuthorsBooks, options => options.MapFrom(MapAtuthorsBooks));

            CreateMap<BookPatchDTO, Book>().ReverseMap();

            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
        }

        private List<BookDTO> MapAuthorDTOBooks(Author author, AuthorDTO authorDTO)
        {
            List<BookDTO> result = new();

            if (author.AuthorsBooks == null)
            {
                return result;
            }

            foreach (var authorBook in author.AuthorsBooks)
            {
                result.Add(new() {
                    Id = authorBook.BookId,
                    Name = authorBook.Book.Name
                });
            }

            return result;
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
