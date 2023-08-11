using ApplyGraduate.Entities.Dtos.StudentDtos;

namespace ApplyGraduate.MVC.Models.ResultModels;

public class HomeIndexResultModel
{
    public StudentGetterDto studentGetterDto { get; set; }
    public AddStudentDto addStudentDto { get; set; }
    public bool IsUpdate { get; set; }
}