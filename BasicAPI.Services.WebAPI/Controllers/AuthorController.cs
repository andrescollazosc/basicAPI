using AutoMapper;
using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAPI.Services.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        #region Attributes
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors
        public AuthorController(IGenericRepository<Author> authorRepository, IMapper mapper) {
            this._authorRepository = authorRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet("authors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAll()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();

                return _mapper.Map<List<AuthorDTO>>(authors);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("author/'{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDTO>> GetById(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);

                if (author is null) {
                    return NotFound();
                }

                return _mapper.Map<AuthorDTO>(author);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDTO>> Post(AuthorCreateDTO authorCreateDTO)
        {
            try
            {
                var author = await _authorRepository.AddAsync(_mapper.Map<Author>(authorCreateDTO));

                if (author is null)
                {
                    return BadRequest();
                }

                return _mapper.Map<AuthorDTO>(author);
            }
            catch (System.Exception)
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
                var result = await _authorRepository.DeleteAsync(id);

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
