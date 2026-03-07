using System.ComponentModel.DataAnnotations;

namespace Nutrilab.Dtos.Auths
{
    public record LoginDto
    {
        [Required]
        [StringLength(200, ErrorMessage = "The {0} field cannot exceed {1} characters.")]
        public string? Email { get; init; }

        [Required]
        public string? Password { get; init; }
    }
}
