using AutoMapper;
using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAPI.Services.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        #region Attributes
        private readonly IBookRepository _bookRepository;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public BookController(IBookRepository bookRepository, IGenericRepository<Author> authorRepository, IMapper mapper)
        {
            this._bookRepository = bookRepository;
            this._authorRepository = authorRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet("books")]
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            try
            {
                var result = await _bookRepository.GetAllAsync();

                return _mapper.Map<List<BookDTO>>(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("books/{authorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllByAuthorId(int authorId)
        {
            try
            {
                var result = await _bookRepository.GetAllByAuthorIdAsync(authorId);

                return _mapper.Map<List<BookDTO>>(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("book/{id:int}")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDTOWithAuthors>> Get(int id)
        {
            try
            {
                var result = await _bookRepository.GetByIdAsync(id);

                if (result is null)
                {
                    return NotFound();
                }

                return _mapper.Map<BookDTOWithAuthors>(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookDTO>> Post(BookCreatedDTO bookCreatedDTO)
        {
            try
            {
                if (bookCreatedDTO.AuthorsId.Count == 0)
                {
                    return BadRequest("Can't create a book without actors");
                }

                var authors = await _authorRepository.ValidateEntitiesIdsAsync(bookCreatedDTO.AuthorsId);

                if (authors.Count != bookCreatedDTO.AuthorsId.Count)
                {
                    return BadRequest("One on more of authores was not found.");
                }

                var book = _mapper.Map<Book>(bookCreatedDTO);
                AddOrderToAuthors(book);

                var result = await _bookRepository.AddAsync(book);
                return _mapper.Map<BookDTO>(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("remove")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _bookRepository.DeleteAsync(id);

                if (!result)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("id:int")]
        public async Task<ActionResult> Put(int id, BookCreatedDTO bookCreatedDTO) {
            var bookDb = await _bookRepository.GetByIdAsync(id);

            if (bookDb is null) {
                return NotFound();
            }

            bookDb = _mapper.Map(bookCreatedDTO, bookDb);
            AddOrderToAuthors(bookDb);

            await _bookRepository.UpdateAsync(bookDb);

            return NoContent();
        }


        [HttpPatch("id:int")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<BookPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var bookDb = await _bookRepository.GetByIdAsync(id);

            if (bookDb is null)
            {
                return NotFound();
            }

            var bookDTO = _mapper.Map<BookPatchDTO>(bookDb);

            patchDocument.ApplyTo(bookDTO, ModelState);

            var isValid = TryValidateModel(bookDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        #endregion

        #region Private methods
        private void AddOrderToAuthors(Book book)
        {
            if (book.AuthorsBooks != null)
            {
                for (int i = 0; i < book.AuthorsBooks.Count; i++)
                {
                    book.AuthorsBooks[i].Order = i;
                }
            }
        }
        #endregion
    }
}
