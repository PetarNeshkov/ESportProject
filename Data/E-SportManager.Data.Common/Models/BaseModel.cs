using System.ComponentModel.DataAnnotations;
using YourMoviesForum.Data.Common.Models;


namespace E_SportManager.Data.Common.Models
{
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        [Key]
        public TKey Id { get; init; }
        public string? CreatedOn { get; set; }
        public string? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedOn { get; set; }
    }
}
