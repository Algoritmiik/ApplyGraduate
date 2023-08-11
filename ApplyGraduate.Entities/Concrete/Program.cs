namespace ApplyGraduate.Entities.Concrete
{
    public class Program : EntityBase
    {
        public string Name { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public List<Student> Students { get; set; }
    }
}