using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.StudentDtos;
using ApplyGraduate.MVC.Models.ResultModels;
using ApplyGraduate.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplyGraduate.MVC.Controllers
{
    public class AttendersController : Controller
    {
        private readonly IGenericService<Student> _studentManager;
        public AttendersController(IGenericService<Student> studentManager)
        {
            _studentManager = studentManager;
        }

        public async Task<IActionResult> Index()
        {
            var studentQuery = _studentManager.SetQuery().Include(s => s.Companions).Include(s => s.Program).ThenInclude(p => p.Unit).ThenInclude(u => u.Faculty);
            var getStudents = await _studentManager.GetAllAsync(studentQuery);
            if (getStudents.ResultStatus == ResultStatus.SUCCESS)
            {
                var students = getStudents.Datas.Where(s => s.IsDeleted == false && s.HaveApplied == true && s.DidJoin == true).ToList();
                AdminPanelIndexResultModel adminPanelIndexResultModel = new();
                foreach (var student in students)
                {
                    adminPanelIndexResultModel.Students.Add(
                        new StudentGetterWithAllFieldsDto
                        {
                            Id = student.Id,
                            Name = student.Name,
                            Surname = student.Surname,
                            Program = student.Program.Name,
                            Unit = student.Program.Unit.Name,
                            Faculty = student.Program.Unit.Faculty.Name,
                            GownRequest = student.GownRequest,
                            GownSize = student.GownSize,
                            DidPaid = student.DidPaid,
                            DidTake = student.DidTake,
                            DidJoin = student.DidJoin,
                            CompanionStatus = student.CompanionStatus,
                            Companions = student.Companions.Where(c => c.IsDeleted == false).ToList(),
                            HaveApplied = student.HaveApplied,
                            Note = student.Note,
                            CreatedDate = student.CreatedDate,
                            ModifiedDate = student.ModifiedDate,
                            IsDeleted = student.IsDeleted
                        }
                    );
                }
                return await Task.Run(() => View(adminPanelIndexResultModel));
            }
            TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
            return await Task.Run(() => RedirectToAction("Error", "Home"));
        }
    }
}