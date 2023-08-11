using ApplyGraduate.Entities.Concrete;

namespace ApplyGraduate.Entities.Dtos.StudentDtos
{
    public class AddStudentDto
    {
        public bool GownRequest { get; set; }
        public string? GownSize { get; set; }
        public bool CompanionStatus { get; set; }
        public List<Companion>? Companions { get; set; }
        public string? Note { get; set; }
    }
}