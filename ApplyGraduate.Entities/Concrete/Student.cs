namespace ApplyGraduate.Entities.Concrete
{
    public class Student : EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ProgramId { get; set; }
        public Program Program { get; set; }
        public bool GownRequest { get; set; }
        public string GownSize { get; set; }
        public bool DidPaid { get; set; }
        public bool DidTake { get; set; }
        public bool CompanionStatus { get; set; }
        public List<Companion> Companions { get; set; }
        public bool DidJoin { get; set; }
        public bool HaveApplied { get; set; }
        public string Note { get; set; }
    }
}