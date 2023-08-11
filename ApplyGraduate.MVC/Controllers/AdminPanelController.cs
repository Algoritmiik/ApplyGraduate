using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.RegisterDateDtos;
using ApplyGraduate.Entities.Dtos.StudentDtos;
using ApplyGraduate.MVC.Models.ResultModels;
using ApplyGraduate.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplyGraduate.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IGenericService<Student> _studentManager;
        private readonly IGenericService<Companion> _companionManager;
        private readonly IGenericService<Dates> _datesManager;
        private readonly IGenericService<ApplyGraduate.Entities.Concrete.Program> _programManager;
        private readonly IGenericService<Unit> _unitManager;
        private readonly IGenericService<Faculty> _facultyManager;
        public AdminPanelController(IGenericService<Student> studentManager, IGenericService<Companion> companionManager, IGenericService<Dates> datesManager, IGenericService<ApplyGraduate.Entities.Concrete.Program> programManager, IGenericService<Unit> unitManager, IGenericService<Faculty> facultyManager)
        {
            _studentManager = studentManager;
            _companionManager = companionManager;
            _datesManager = datesManager;
            _programManager = programManager;
            _unitManager = unitManager;
            _facultyManager = facultyManager;
        }

        public async Task<IActionResult> Index()
        {
            var getStudents = await _studentManager.GetAllByNonDeletedAsync(s => s.Companions);
            if (getStudents.ResultStatus == ResultStatus.SUCCESS)
            {
                var students = getStudents.Datas.Where(s => s.HaveApplied == true).ToList();
                AdminPanelIndexResultModel adminPanelIndexResultModel = new();
                foreach (var student in students)
                {
                    var getProgram = await _programManager.GetByIdAsync(student.ProgramId);
                    if (getProgram.ResultStatus == ResultStatus.SUCCESS)
                    {
                        var getUnit = await _unitManager.GetByIdAsync(getProgram.Data.UnitId);
                        if (getUnit.ResultStatus == ResultStatus.SUCCESS)
                        {
                            var getFaculty = await _facultyManager.GetByIdAsync(getUnit.Data.FacultyId);
                            if (getFaculty.ResultStatus == ResultStatus.SUCCESS)
                            {
                                adminPanelIndexResultModel.Students.Add(
                                    new StudentGetterWithAllFieldsDto
                                    {
                                        Id = student.Id,
                                        Name = student.Name,
                                        Surname = student.Surname,
                                        Program = getProgram.Data.Name,
                                        Unit = getUnit.Data.Name,
                                        Faculty = getFaculty.Data.Name,
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
                            else
                            {
                                TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
                                return await Task.Run(() => RedirectToAction("Error", "Home"));
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
                            return await Task.Run(() => RedirectToAction("Error", "Home"));
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
                        return await Task.Run(() => RedirectToAction("Error", "Home"));
                    }
                }
                return await Task.Run(() => View(adminPanelIndexResultModel));
            }
            TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
            return await Task.Run(() => RedirectToAction("Error", "Home"));
        }

        public async Task<IActionResult> GownOps()
        {
            var getStudents = await _studentManager.GetAllByNonDeletedAsync(s => s.Companions);
            if (getStudents.ResultStatus == ResultStatus.SUCCESS)
            {
                var students = getStudents.Datas.Where(s => s.HaveApplied == true).Where(s => s.GownRequest == true).ToList();
                AdminPanelIndexResultModel adminPanelIndexResultModel = new();
                foreach (var student in students)
                {
                    var getProgram = await _programManager.GetByIdAsync(student.ProgramId);
                    if (getProgram.ResultStatus == ResultStatus.SUCCESS)
                    {
                        var getUnit = await _unitManager.GetByIdAsync(getProgram.Data.UnitId);
                        if (getUnit.ResultStatus == ResultStatus.SUCCESS)
                        {
                            var getFaculty = await _facultyManager.GetByIdAsync(getUnit.Data.FacultyId);
                            if (getFaculty.ResultStatus == ResultStatus.SUCCESS)
                            {
                                adminPanelIndexResultModel.Students.Add(
                                    new StudentGetterWithAllFieldsDto
                                    {
                                        Id = student.Id,
                                        Name = student.Name,
                                        Surname = student.Surname,
                                        Program = getProgram.Data.Name,
                                        Unit = getUnit.Data.Name,
                                        Faculty = getFaculty.Data.Name,
                                        GownRequest = student.GownRequest,
                                        GownSize = student.GownSize,
                                        DidPaid = student.DidPaid,
                                        DidTake = student.DidTake,
                                        DidJoin = student.DidJoin,
                                        CompanionStatus = student.CompanionStatus,
                                        HaveApplied = student.HaveApplied,
                                        Note = student.Note,
                                        CreatedDate = student.CreatedDate,
                                        ModifiedDate = student.ModifiedDate,
                                        IsDeleted = student.IsDeleted
                                    }
                                );
                            }
                            else
                            {
                                TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
                                return await Task.Run(() => RedirectToAction("Error", "Home"));
                            }
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
                            return await Task.Run(() => RedirectToAction("Error", "Home"));
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
                        return await Task.Run(() => RedirectToAction("Error", "Home"));
                    }
                }
                return await Task.Run(() => View(adminPanelIndexResultModel));
            }
            TempData["ErrorMessage"] = $"Veriler getirilirken bir hatayla karşılaşıldı!";
            return await Task.Run(() => RedirectToAction("Error", "Home"));
        }

        public async Task<IActionResult> Settings()
        {
            var getDates = await _datesManager.GetAllByNonDeletedAsync();
            if (getDates.ResultStatus == ResultStatus.SUCCESS)
            {
                var dates = getDates.Datas;
                if (dates != null && dates.Count > 0)
                {
                    var date = dates.OrderByDescending(d => d.CreatedDate).FirstOrDefault();
                    return await Task.Run(() => View(new AdminPanelSettingsResultModel
                    {
                        addRegisterDateDto = new AddRegisterDateDto
                        {
                            RegisterBeginningDate = date.RegisterBeginningDate,
                            RegisterEndingDate = date.RegisterEndingDate,
                            GraduationDate = date.GraduationDate
                        }
                    }));
                }
                return await Task.Run(() => View());
            }
            TempData["ErrorMessage"] = "Veriler getirilirken bir hatayla karşılaşıldı!";
            return await Task.Run(() => RedirectToAction("Error", "Home"));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDate(AddRegisterDateDto addRegisterDateDto)
        {
            addRegisterDateDto.GraduationDate = addRegisterDateDto.GraduationDate < addRegisterDateDto.RegisterEndingDate ? addRegisterDateDto.RegisterEndingDate : addRegisterDateDto.GraduationDate;

            Dates registerDate = new Dates
            {
                RegisterBeginningDate = addRegisterDateDto.RegisterBeginningDate,
                RegisterEndingDate = addRegisterDateDto.RegisterEndingDate,
                GraduationDate = addRegisterDateDto.GraduationDate,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            };

            var addDate = await _datesManager.AddAsync(registerDate);
            if (addDate.ResultStatus == ResultStatus.SUCCESS)
            {
                TempData["SuccessMessage"] = "Kayıt Tarihi Başarıyla Kaydedildi";
                return await Task.Run(() => RedirectToAction("Settings"));
            }
            TempData["ErrorMessage"] = "Kayıt Tarihi Kaydedilirken Bir Hata Meydana Geldi!";
            return await Task.Run(() => RedirectToAction("Settings"));
        }

        public async Task<IActionResult> StudentJoined(int id)
        {
            var getStudent = await _studentManager.GetByIdAsync(id);
            if (getStudent.ResultStatus == ResultStatus.SUCCESS)
            {
                var student = getStudent.Data;
                if (student != null && student.DidJoin == false)
                {
                    student.DidJoin = true;
                    var updateStudent = await _studentManager.UpdateAsync(student);
                    if (updateStudent.ResultStatus != ResultStatus.SUCCESS)
                    {
                        TempData["ErrorMessage"] = "Öğrencinin Katılım DUrumu Kaydedilemedi!";
                        return await Task.Run(() => View("Index"));
                    }
                }
                return await Task.Run(() => RedirectToAction("Index"));
            }
            TempData["ErrorMessage"] = "Veriler getirilirken bir hatayla karşılaşıldı!";
            return await Task.Run(() => View("Index"));
        }

        public async Task<IActionResult> CompanionJoined(int id)
        {
            var getCompanion = await _companionManager.GetByIdAsync(id);
            if (getCompanion.ResultStatus == ResultStatus.SUCCESS)
            {
                var companion = getCompanion.Data;
                var getStudent = await _studentManager.GetByIdAsync(companion.StudentId);
                if (getStudent.ResultStatus == ResultStatus.SUCCESS)
                {
                    var student = getStudent.Data;
                    if (companion != null)
                    {
                        if (companion.DidJoin == false)
                        {
                            if (student.DidJoin == true)
                            {
                                companion.DidJoin = true;
                                var updateCompanion = await _companionManager.UpdateAsync(companion);
                                if (updateCompanion.ResultStatus == ResultStatus.SUCCESS)
                                {
                                    TempData["SuccessMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin, <b>{companion.Name} {companion.Surname}</b> isimli refakatçisinin katılım durumu başarıyla kaydedildi!";
                                    return await Task.Run(() => RedirectToAction("Index"));
                                }
                                TempData["ErrorMessage"] = "Refakatçi katılım durumu kaydedilirken bir hata ile karşılaşıldı!";
                                return await Task.Run(() => RedirectToAction("Index"));
                            }
                            TempData["ErrorMessage"] = $"<b>{companion.Name} {companion.Surname}</b> isimli refakatçinin davetçi öğrencisi katılım göstermedi!";
                            return await Task.Run(() => RedirectToAction("Index"));
                        }
                        TempData["ErrorMessage"] = $"<b>{companion.Name} {companion.Surname}</b> isimli refakatçi zaten katılım göstermiş durumda!";
                        return await Task.Run(() => RedirectToAction("Index"));
                    }
                    TempData["ErrorMessage"] = $"Böyle bir refakatçi bulunamadı!";
                    return await Task.Run(() => RedirectToAction("Index"));
                }
                TempData["ErrorMessage"] = "Öğrencinin verileri getirilirken bir hata ile karşılaşıldı!";
                return await Task.Run(() => View("Index"));
            }
            TempData["ErrorMessage"] = "Refakatçinin verileri getirilirken bir hatayla karşılaşıldı!";
            return await Task.Run(() => View("Index"));
        }

        public async Task<IActionResult> GownPayment(int id)
        {
            var getStudent = await _studentManager.GetByIdAsync(id);
            if (getStudent.ResultStatus == ResultStatus.SUCCESS)
            {
                var student = getStudent.Data;
                if (student != null)
                {
                    if (student.GownRequest == false)
                    {
                        TempData["ErrorMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin cübbe talebi bulunmamaktadır!";
                        return await Task.Run(() => RedirectToAction("GownOps"));
                    }

                    if (student.DidPaid == false)
                    {
                        student.DidPaid = true;
                        var updateStudent = await _studentManager.UpdateAsync(student);
                        if (updateStudent.ResultStatus == ResultStatus.SUCCESS)
                        {
                            TempData["SuccessMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin cübbe ödemesi başarıyla alındı!";
                            return await Task.Run(() => RedirectToAction("GownOps"));
                        }
                        TempData["ErrorMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin cübbe ödemesi alınırken bir hata ile karşılaşıldı!";
                        return await Task.Run(() => RedirectToAction("GownOps"));
                    }
                    TempData["ErrorMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrenci, cübbe ödemesini zaten gerçekleştirmiş durumda!";
                    return await Task.Run(() => RedirectToAction("GownOps"));
                }
                TempData["ErrorMessage"] = $"Böyle bir kullanıcı bulunamadı!";
                return await Task.Run(() => RedirectToAction("GownOps"));
            }
            TempData["ErrorMessage"] = $"Böyle bir öğrenci bulunamadı!";
            return await Task.Run(() => RedirectToAction("GownOps"));
        }

        public async Task<IActionResult> TakeGown(int id)
        {
            var getStudent = await _studentManager.GetByIdAsync(id);
            if (getStudent.ResultStatus == ResultStatus.SUCCESS)
            {
                var getDates = await _datesManager.GetAllByNonDeletedAsync();
                if (getDates.ResultStatus == ResultStatus.SUCCESS)
                {
                    var student = getStudent.Data;
                    var dates = getDates.Datas;
                    var graduationDate = dates.OrderByDescending(d => d.CreatedDate).FirstOrDefault().GraduationDate;

                    if (student != null)
                    {
                        if (student.GownRequest == false)
                        {
                            TempData["ErrorMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin cübbe talebi bulunmamaktadır!";
                            return await Task.Run(() => RedirectToAction("GownOps"));
                        }

                        if (student.DidPaid != false)
                        {
                            student.DidTake = true;
                            var updateStudent = await _studentManager.UpdateAsync(student);
                            if (updateStudent.ResultStatus == ResultStatus.SUCCESS)
                            {
                                TempData["SuccessMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin cübbe teslimi başarıyla kaydedilmiştir!";
                                return await Task.Run(() => RedirectToAction("GownOps"));
                            }
                            TempData["ErrorMessage"] = "Cübbe teslim işleminin kaydedilmesi sırasında bir hata ile karşılaşıldı!";
                            return await Task.Run(() => RedirectToAction("GownOps"));
                        }
                        TempData["ErrorMessage"] = $"<b>{student.Name} {student.Surname}</b> isimli öğrencinin cübbe ödemesi henüz alınmamıştır!";
                        return await Task.Run(() => RedirectToAction("GownOps"));
                    }
                    TempData["ErrorMessage"] = $"Böyle bir kullanıcı bulunamadı!";
                    return await Task.Run(() => RedirectToAction("GownOps"));
                }
                TempData["ErrorMessage"] = "Tarih verileri getirilirken bir hata ile karşılaşıldı!";
                return await Task.Run(() => RedirectToAction("GownOps"));
            }
            TempData["ErrorMessage"] = $"Böyle bir öğrenci bulunamadı!";
            return await Task.Run(() => RedirectToAction("GownOps"));
        }
    }
}