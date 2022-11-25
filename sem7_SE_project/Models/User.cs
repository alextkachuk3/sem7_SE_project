using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Login { get; set; }

        [Required]
        [StringLength(100)]
        public string? Password { get; set; }

        public List<Car>? TestDrivedCars { get; set; }

        public bool CheckPassword(string? password)
        {
            return Password!.Equals(password);
        }

    }
}
