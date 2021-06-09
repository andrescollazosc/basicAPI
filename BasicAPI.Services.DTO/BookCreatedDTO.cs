﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Services.DTO
{
    public class BookCreatedDTO
    {

        [MaxLength(60)]
        [Required(ErrorMessage = "The field (Name) is Required.")]
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
    }
}
