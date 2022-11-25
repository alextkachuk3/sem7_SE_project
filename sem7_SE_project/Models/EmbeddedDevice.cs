using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    public class EmbeddedDevice
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public List<Car>? Cars { get; set; }
    }
}
