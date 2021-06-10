using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Services.DTO
{
    public class CommentCreateDTO
    {
        [Required(ErrorMessage = "Fiel (Description) is required.")]
        public string Description { get; set; }
    }
}
