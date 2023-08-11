namespace ApplyGraduate.Entities.Concrete
{
    public class Companion : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string HesCode { get; set; }
        public string PhoneNumber { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public bool DidJoin { get; set; }
    }
}