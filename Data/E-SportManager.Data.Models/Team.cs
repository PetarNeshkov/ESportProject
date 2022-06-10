
using E_SportManager.Data.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace E_SportManager.Data
{
    public class Team:BaseModel<int>
    {
        public Team()
        {
            Players=new HashSet<Player>();
        }

        [Required]
        public string Title { get; set; }

        public int Rating { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
