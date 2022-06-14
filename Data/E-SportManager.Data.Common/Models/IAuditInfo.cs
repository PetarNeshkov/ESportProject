

namespace E_SportManager.Data.Common.Models
{
    public interface IAuditInfo
    {
        string CreatedOn { get; set; }

        string ModifiedOn { get; set; }

        bool IsDeleted { get; set; }

        string DeletedOn { get; set; }
    }
}
