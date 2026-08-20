namespace KiaKooshar.Domain.Entities.BaseEntities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
