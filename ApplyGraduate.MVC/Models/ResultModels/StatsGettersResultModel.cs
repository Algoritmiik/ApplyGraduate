using ApplyGraduate.Entities.Dtos.StatsDto;

namespace ApplyGraduate.MVC.Models.ResultModels
{
    public class StatsGettersResultModel
    {
        public StatsStudentsGetterDto statsStudentsGetterDto { get; set; }
        public StatsCompanionsGetterDto statsCompanionsGetterDto { get; set; }
        public StatFacultiesGetterDto statFacultiesGetterDto { get; set; }
        public StatsUnitsGetterDto statsUnitsGetterDto { get; set; }
        public StatsProgramsGetterDto statsProgramsGetterDto { get; set; }
        public int FacultySelect { get; set; } = -1;
        public int UnitSelect { get; set; } = -1;
        public int ProgramSelect { get; set; } = -1;
    }
}