using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Services.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
    }
}
