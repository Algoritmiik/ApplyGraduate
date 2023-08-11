using ApplyGraduate.Entities.Concrete;

namespace ApplyGraduate.Entities.Dtos.StudentDtos
{
    public class StudentGetterWithAllFieldsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Program { get; set; }
        public string Unit { get; set; }
        public string Faculty { get; set; }
        public bool GownRequest { get; set; }
        public string GownSize { get; set; }
        public bool DidPaid { get; set; }
        public bool DidTake { get; set; }
        public bool CompanionStatus { get; set; }
        public List<Companion> Companions { get; set; }
        public bool DidJoin { get; set; }
        public bool HaveApplied { get; set; }
        public string Note { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}