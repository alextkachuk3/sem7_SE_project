using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public Car? Car { get; set; }

        [Required]
        public Client? Client { get; set; }

        [Required]
        public bool IsTestDriveNeeded { get; set; }

        [Required]
        public DateTime CreationDateTime { get; set; }

    }
}
