
using System.ComponentModel.DataAnnotations;

namespace E_SportManager.Data
{
    public class Team
    {
        public Team()
        {
            Players=new HashSet<Player>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; }

        public int Rating { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
