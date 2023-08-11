namespace ApplyGraduate.Entities.Concrete
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}