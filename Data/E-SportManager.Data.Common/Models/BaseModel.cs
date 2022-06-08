using System.ComponentModel.DataAnnotations;
using YourMoviesForum.Data.Common.Models;

using static E_SportManager.Data.Common.DataValidation;

namespace E_SportManager.Data.Common.Models
{
    public class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedOn { get; set; }
    }
}
