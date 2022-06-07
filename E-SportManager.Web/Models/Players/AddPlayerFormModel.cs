using E_SportManager.Data.enums;
using System.ComponentModel.DataAnnotations;

namespace E_SportManager.Models.Players
{
    public class AddPlayerFormModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Role { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [Required]
        public string Description { get; set; }

        public Division Division { get; set; }
    }
}
