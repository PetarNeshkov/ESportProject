using E_SportManager.Data.Common.Models;
using E_SportManager.Data.enums;
using System.ComponentModel.DataAnnotations;

using static E_SportManager.Data.Common.DataValidation.Player;

namespace E_SportManager.Data
{
    public class Player:BaseModel<string>
    {

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Range(0,50)]
        public int YearsOfExperience { get; set; }

        public Role Role { get; set; }

        public Division Division { get; set; }

        [Required]
        [MaxLength(PlayerDescriptionMaxLength)]
        public string Description { get; set; }



    }
}