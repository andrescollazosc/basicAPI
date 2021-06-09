using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicApi.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [MaxLength(15)]
        public string Passport { get; set; }

        [MaxLength(60)]
        public string FirstName { get; set; }

        [MaxLength(60)]
        public string LasttName { get; set; }

        public bool Active { get; set; }

        public List<Book> Books { get; set; }
    }
}
