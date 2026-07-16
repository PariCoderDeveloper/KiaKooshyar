namespace KiaKooshar.Domain.Entities.BaseEntities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted{ get; set; }
    }
    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
