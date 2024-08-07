namespace ACMESchool.Domain.Entities
{
    public class Course : BaseEntity
    {
        public decimal Fee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
