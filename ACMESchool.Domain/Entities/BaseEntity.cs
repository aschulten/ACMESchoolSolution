namespace ACMESchool.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
