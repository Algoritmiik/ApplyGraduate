namespace ApplyGraduate.Entities.Concrete
{
    public class Unit : EntityBase
    {
        public string Name { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public List<Program> Programs { get; set; }
    }
}