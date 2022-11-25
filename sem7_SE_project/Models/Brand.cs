using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
    }
}
