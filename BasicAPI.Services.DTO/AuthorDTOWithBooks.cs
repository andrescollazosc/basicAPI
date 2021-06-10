using System.Collections.Generic;

namespace BasicAPI.Services.DTO
{
    public class AuthorDTOWithBooks : AuthorDTO
    {
        public List<BookDTO> Books { get; set; }
    }
}
