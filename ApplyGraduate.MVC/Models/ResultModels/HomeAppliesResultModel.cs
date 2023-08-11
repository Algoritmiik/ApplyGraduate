using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.CompanionDtos;

namespace ApplyGraduate.MVC.Models.ResultModels
{
    public class HomeAppliesResultModel
    {
        public Student Student { get; set; }
        public AddCompanionDto addCompanionDto   { get; set; }
    }
}