using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace KiaKooshar.Infrastructure.AuditLog
{
    public class AuditEntry
    {
        public AuditEntry (
            EntityEntry entry
            )
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string TableName { get; set; } = null!;
        public Dictionary<string, object?> KeyValues { get; } = new ();
        public Dictionary<string, object?> OldValues { get; } = new ();
        public Dictionary<string, object?> NewValues { get; } = new ();
        public List<string> ChangedColumns { get; } = new ();
        public string ChangeType { get; set; } = null!;
        public bool HasTemporaryProperties { get; set; }
        public List<PropertyEntry> TemporaryProperties { get; } = new ();
        public long? UserId { get; set; }
        public string? Username { get; set; }
        public string? IP { get; set; }
        public KiaKooshar.Domain.Entities.Audit.AuditLog ToAuditLog ()
        {
            return new KiaKooshar.Domain.Entities.Audit.AuditLog
            {
                TableName = TableName,
                KeyValues = JsonSerializer.Serialize (KeyValues),
                OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize (OldValues),
                NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize (NewValues),
                ChangedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize (ChangedColumns),
                ChangeType = ChangeType,
                UserId = UserId,
                Username = Username,
                IP = IP,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
