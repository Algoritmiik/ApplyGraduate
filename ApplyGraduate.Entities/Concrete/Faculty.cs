namespace ApplyGraduate.Entities.Concrete
{
    public class Faculty : EntityBase
    {
        public string Name { get; set; }
        public List<Unit> Units { get; set; }
    }
}