using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicApi.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(60)]
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }
        
        public int AuthorId { get; set; }
        
        public bool Active { get; set; }

        public Author Author { get; set; }
        public List<Comment>  Comments { get; set; }
    }
}
