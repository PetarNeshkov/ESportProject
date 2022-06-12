using E_SportManager.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

using static E_SportManager.Data.Common.DataValidation.Team;

namespace E_SportManager.Data
{
    public class Team:BaseModel<int>
    {
        [Required]
        [MaxLength(TeamNameMaxLength)]
        public string Title { get; set; }

        public int Rating { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int MidLanerId { get; set; }

        public Player MidLaner { get; set; }


        public int TopLanerId { get; set; }

        public Player TopLaner { get; set; }

        public int BottomLanerId { get; set; }

        public Player BottomLaner { get; set; }

        public int SupportLanerId { get; set; }

        public Player SupportLaner { get; set; }

        public int JungleLanerId { get; set; }

        public Player JungleLaner { get; set; }
    }
}
