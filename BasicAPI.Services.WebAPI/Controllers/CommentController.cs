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
    [Route("api/book/{bookId:int}/commnet")]
    public class CommentController : ControllerBase
    {
        #region Attributes
        private readonly ICommentRepository _commentRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CommentController(ICommentRepository commentRepository, IBookRepository bookRepository, 
            IMapper mapper)
        {
            this._commentRepository = commentRepository;
            this._bookRepository = bookRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Actions

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> Get(int bookId)
        {
            var exitsBook = await _bookRepository.GetByIdAsync(bookId);

            if (exitsBook is null)
            {
                return NotFound();
            }

            var comments = await _commentRepository.GetByBookIdAsync(bookId);

            return _mapper.Map<List<CommentDto>>(comments);
        }

        [HttpGet("{id:int}", Name = "GetComment")]
        public async Task<ActionResult<CommentDto>> GetById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment is null)
            {
                return NotFound();
            }

            return _mapper.Map<CommentDto>(comment);
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> Post(int bookId, CommentCreateDTO commentCreateDTO)
        {
            var exitsBook = await _bookRepository.GetByIdAsync(bookId);

            if (exitsBook is null)
            {
                return NotFound();
            }

            var comment = _mapper.Map<Comment>(commentCreateDTO);
            comment.BookId = bookId;

            var result = await _commentRepository.AddAsync(comment);
            var commenDTO = _mapper.Map<CommentDto>(result);

            return CreatedAtRoute("GetComment", new { id = comment.Id, bookId = bookId }, commenDTO);
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(int bookId, int id, CommentCreateDTO commentCreateDTO)
        {
            try
            {
                var exitsBook = await _bookRepository.GetByIdAsync(bookId);

                if (exitsBook is null)
                {
                    return NotFound();
                }

                var exits = await _commentRepository.GetByIdAsync(id);

                if (exits is null)
                {
                    return NotFound();
                }

                var comment = _mapper.Map<Comment>(commentCreateDTO);
                comment.Id = id;
                comment.BookId = bookId;

                await _commentRepository.UpdateAsync(comment);

                return NoContent();

            }
            catch (System.Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion


    }
}
