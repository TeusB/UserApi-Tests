using System.ComponentModel.DataAnnotations;

namespace user.DTO
{
    public class UpdateUser
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? UserName { get; set; }
    }
}
