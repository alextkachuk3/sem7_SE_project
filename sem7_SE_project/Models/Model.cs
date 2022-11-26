using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    public class Model
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public int FuelCapacity { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        public Brand? Brand { get; set; }

        public List<Car>? Cars { get; set; }
    }
}
