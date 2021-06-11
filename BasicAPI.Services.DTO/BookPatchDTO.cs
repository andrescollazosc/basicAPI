using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Services.DTO
{
    public class BookPatchDTO
    {
        [MaxLength(60)]
        [Required(ErrorMessage = "The field (Name) is Required.")]
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
