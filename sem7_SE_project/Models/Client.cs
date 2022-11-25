using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }
    }
}
