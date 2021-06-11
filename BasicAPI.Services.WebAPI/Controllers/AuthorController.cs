using AutoMapper;
using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        #endregion

        #region Constructors
        public AuthorController(IGenericRepository<Author> authorRepository, IMapper mapper, 
            IConfiguration configuration) {
            this._authorRepository = authorRepository;
            this._mapper = mapper;
            this.configuration = configuration;
        }
        #endregion

        #region Actions

        [HttpGet("config")]
        public ActionResult<string> GetConfig()
        {
            return configuration["lastName"];
        }

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
        public async Task<ActionResult<AuthorDTOWithBooks>> GetById(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);

                if (author is null) {
                    return NotFound();
                }

                return _mapper.Map<AuthorDTOWithBooks>(author);
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

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthorDTO>> Put(AuthorCreateDTO authorCreateDTO, int id)
        {
            try
            {
                var exits = await _authorRepository.GetByIdAsync(id);

                if (exits is null)
                {
                    return NotFound();
                }

                var author = _mapper.Map<Author>(authorCreateDTO);
                author.Id = id;

                await _authorRepository.UpdateAsync(author);

                return NoContent();

            }
            catch (System.Exception ex)
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
