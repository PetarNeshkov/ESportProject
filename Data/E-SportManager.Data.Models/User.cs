using Microsoft.AspNetCore.Identity;
using YourMoviesForum.Data.Common.Models;

namespace E_SportManager.Data
{
    public class User:IdentityUser, IAuditInfo
    {
        public User()
        {
            Teams=new HashSet<Team>();
        }

        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedOn { get; set; }

        public ICollection<Team> Teams { get; init; }
    }
}
