using System.ComponentModel.DataAnnotations;

namespace user.DTO
{
    public class InsertUser
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
