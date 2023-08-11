using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.StudentDtos;

namespace ApplyGraduate.MVC.Models.ResultModels
{
    public class AdminPanelIndexResultModel
    {
        public List<StudentGetterWithAllFieldsDto> Students { get; set; } = new();
    }
}