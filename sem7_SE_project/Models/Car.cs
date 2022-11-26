using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace sem7_SE_project.Models
{
    [Index(nameof(Price), IsUnique = false)]
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public Model? Model { get; set; }

        [Required]
        public string? RegistrationNumber { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Mileage { get; set; }

        [Required]
        public EngineType? EngineType { get; set; }

        public List<EmbeddedDevice>? EmbeddedDevices { get; set; }

        public List<Client>? UsedMilage { get; set; }

    }
}
