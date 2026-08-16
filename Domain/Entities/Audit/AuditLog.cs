using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Audit
{
    public class AuditLog : BaseEntity
    {
        public string TableName { get; set; } = null!;
        public string KeyValues { get; set; } = null!;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string ChangeType { get; set; } = null!;
        public string? ChangedColumns { get; set; }
        public long? UserId { get; set; }
        public string? Username { get; set; }
        public string? IP { get; set; }
    }
}
