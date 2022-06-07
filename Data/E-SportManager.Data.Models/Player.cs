using E_SportManager.Data.enums;
using System.ComponentModel.DataAnnotations;

namespace E_SportManager.Data
{
    public class Player
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Range(0,50)]
        public int YearsOfExperience { get; set; }

        public Role Role { get; set; }

        public Division Division { get; set; }

        [Required]
        public string Description { get; set; }

    }
}