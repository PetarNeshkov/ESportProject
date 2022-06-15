using E_SportManager.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

using static E_SportManager.Data.Common.DataValidation.Player;

namespace E_SportManager.Data
{
    public class Player:BaseModel<int>
    {
        public Player()
        {
            MidLaners= new HashSet<Team>();
            TopLaners= new HashSet<Team>();
            JungleLaners= new HashSet<Team>();
            BottomLaners= new HashSet<Team>();
            SupportLaners= new HashSet<Team>();
        }

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Range(0,50)]
        public int YearsOfExperience { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string Division { get; set; }

        [Required]
        [MaxLength(PlayerDescriptionMaxLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Team> MidLaners { get; init; }
        public ICollection<Team> TopLaners { get; init; }
        public ICollection<Team> JungleLaners { get; init; }
        public ICollection<Team> BottomLaners { get; init; }
        public ICollection<Team> SupportLaners { get; init; }

    }
}