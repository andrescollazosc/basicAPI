using AutoMapper;
using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            try
            {
                var result = await _bookRepository.GetByIdAsync(id);

                if (result is null)
                {
                    return NotFound();
                }

                return _mapper.Map<BookDTO>(result);
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
                var author = await _authorRepository.GetByIdAsync(bookCreatedDTO.AuthorId);

                if (author is null)
                {
                    return BadRequest($"The author with Id: { bookCreatedDTO.AuthorId } was not found.");
                }

                var result = await _bookRepository.AddAsync(_mapper.Map<Book>(bookCreatedDTO));

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
        #endregion
    }
}
