using AutoMapper;
using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using BasicAPI.Services.DTO;
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

        [HttpPost]
        public async Task<ActionResult> Post(int bookId, CommentCreateDTO commentCreateDTO)
        {
            var exitsBook = await _bookRepository.GetByIdAsync(bookId);

            if (exitsBook is null)
            {
                return NotFound();
            }

            var comment = _mapper.Map<Comment>(commentCreateDTO);
            comment.BookId = bookId;

            await _commentRepository.AddAsync(comment);

            return Ok();
        }
        #endregion


    }
}
