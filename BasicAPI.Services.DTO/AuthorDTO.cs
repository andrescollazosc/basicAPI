using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Services.DTO
{
    public class AuthorCreateDTO
    {

        [MaxLength(15)]
        [Required(ErrorMessage = "Fiels (Passport) is Required.")]
        public string Passport { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Fiels (FirstName) is Required.")]
        public string FirstName { get; set; }

        [MaxLength(60)]
        [Required(ErrorMessage = "Fiels (LasttName) is Required.")]
        public string LasttName { get; set; }
    }


    public class AuthorDTO
    {
        public int Id { get; set; }

        public string Passport { get; set; }

        public string FirstName { get; set; }
        
        public string LasttName { get; set; }
    }
}
