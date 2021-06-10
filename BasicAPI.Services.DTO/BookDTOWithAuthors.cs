using System.Collections.Generic;

namespace BasicAPI.Services.DTO
{
    public class BookDTOWithAuthors : BookDTO
    {
        public List<AuthorDTO> Authors { get; set; }
    }
}
