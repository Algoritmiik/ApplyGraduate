using Microsoft.AspNetCore.Mvc;
using ApplyGraduate.Services.Abstract;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.StudentDtos;
using ApplyGraduate.MVC.Models.ResultModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplyGraduate.Entities.Dtos.CompanionDtos;
using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace ApplyGraduate.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGenericService<Student> _studentManager;
    private readonly IGenericService<Companion> _companionManager;
    private readonly UserManager<User> _userManager;
    private readonly IGenericService<GownSize> _gownSizeManager;
    private readonly IGenericService<Dates> _datesManager;
    private readonly IGenericService<ApplyGraduate.Entities.Concrete.Program> _programManager;
    private readonly IGenericService<Unit> _unitManager;
    private readonly IGenericService<Faculty> _facultyManager;

    public HomeController(ILogger<HomeController> logger, IGenericService<Student> studentManager, UserManager<User> userManager, IGenericService<GownSize> gownSizeManager, IGenericService<Companion> companionManager, IGenericService<Dates> datesManager, IGenericService<ApplyGraduate.Entities.Concrete.Program> programManager, IGenericService<Unit> unitManager, IGenericService<Faculty> facultyManager)
    {
        _logger = logger;
        _studentManager = studentManager;
        _userManager = userManager;
        _gownSizeManager = gownSizeManager;
        _companionManager = companionManager;
        _datesManager = datesManager;
        _programManager = programManager;
        _unitManager = unitManager;
        _facultyManager = facultyManager;
    }

    public async Task<IActionResult> Index()
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        var getStudent = await _studentManager.GetByIdAsync(5, s => s.Companions);
        if (getStudent.ResultStatus == ResultStatus.SUCCESS)
        {
            var student = getStudent.Data;
            var getProgram = await _programManager.GetByIdAsync(student.ProgramId);
            if (getProgram.ResultStatus == ResultStatus.SUCCESS)
            {
                var getUnit = await _unitManager.GetByIdAsync(getProgram.Data.UnitId);
                if (getUnit.ResultStatus == ResultStatus.SUCCESS)
                {
                    var getFaculty = await _facultyManager.GetByIdAsync(getUnit.Data.FacultyId);
                    if (getFaculty.ResultStatus == ResultStatus.SUCCESS)
                    {
                        if (student.Companions != null && student.Companions.Count > 0)
                            student.Companions = student.Companions.Where(c => c.IsDeleted == false).ToList();

                        HomeIndexResultModel homeIndexResultModel = new HomeIndexResultModel
                        {
                            studentGetterDto = new StudentGetterDto
                            {
                                Name = student.Name,
                                Surname = student.Surname,
                                Faculty = getFaculty.Data.Name,
                                Unit = getUnit.Data.Name,
                                Program = getProgram.Data.Name
                            },
                            addStudentDto = new AddStudentDto()
                        };

                        if (student.HaveApplied == false)
                        {
                            var getGownSizes = await _gownSizeManager.GetAllByNonDeletedAsync();
                            if (getGownSizes.ResultStatus == ResultStatus.SUCCESS)
                                ViewBag.GownSizes = new SelectList(getGownSizes.Datas, "Size", "Size");
                            else
                                return await Task.Run(() => View("Error"));

                            return await Task.Run(() => View(homeIndexResultModel));
                        }
                        return await Task.Run(() => View("Applies", new HomeAppliesResultModel { Student = student }));
                    }
                    return await Task.Run(() => View("Error"));
                }
                return await Task.Run(() => View("Error"));
            }
            return await Task.Run(() => View("Error"));
        }
        return await Task.Run(() => View("Error"));
    }

    [HttpPost]
    public async Task<IActionResult> Index(AddStudentDto addStudentDto)
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        var getStudent = await _studentManager.GetByIdAsync(5);
        if (getStudent.ResultStatus == ResultStatus.SUCCESS)
        {
            var student = getStudent.Data;
            var haveAppliedStatus = getStudent.Data.HaveApplied;
            var getProgram = await _programManager.GetByIdAsync(student.ProgramId);
            if (getProgram.ResultStatus == ResultStatus.SUCCESS)
            {
                var getUnit = await _unitManager.GetByIdAsync(getProgram.Data.UnitId);
                if (getUnit.ResultStatus == ResultStatus.SUCCESS)
                {
                    var getFaculty = await _facultyManager.GetByIdAsync(getUnit.Data.FacultyId);
                    if (getFaculty.ResultStatus == ResultStatus.SUCCESS)
                    {
                        if (addStudentDto.GownRequest == true && addStudentDto.GownSize == null)
                        {
                            ModelState.AddModelError("Error", "Lütfen cübbe ölçüsünü seçiniz");
                            var getGownSizes = await _gownSizeManager.GetAllByNonDeletedAsync();
                            if (getGownSizes.ResultStatus == ResultStatus.SUCCESS)
                                ViewBag.GownSizes = new SelectList(getGownSizes.Datas, "Size", "Size");
                            else
                                return await Task.Run(() => View("Error"));

                            return await Task.Run(() => View(new HomeIndexResultModel
                            {
                                studentGetterDto = new StudentGetterDto
                                {
                                    Name = student.Name,
                                    Surname = student.Surname,
                                    Faculty = getFaculty.Data.Name,
                                    Unit = getUnit.Data.Name,
                                    Program = getProgram.Data.Name
                                }
                            }));
                        }

                        if (ModelState.IsValid)
                        {
                            student.GownRequest = addStudentDto.GownRequest;
                            student.GownSize = addStudentDto.GownRequest != false ? addStudentDto.GownSize : null;
                            student.Note = addStudentDto.Note;
                            student.HaveApplied = true;
                            student.IsDeleted = false;
                            student.ModifiedDate = DateTime.Now;

                            var studentUpdate = await _studentManager.UpdateAsync(student);
                            if (studentUpdate.ResultStatus == ResultStatus.SUCCESS)
                            {
                                if (haveAppliedStatus == false && student.HaveApplied == true)
                                    await InvitationSenderAsync(student.Name, student.Surname);

                                TempData["SuccessMessage"] = "Bilgileriniz başarıyla kaydedilmiştir!";
                                return await Task.Run(() => RedirectToAction("Index"));
                            }

                            TempData["ErrorMessage"] = "İşlem sırasında bir hata meydana geldi!";
                            return await Task.Run(() => RedirectToAction("Index"));
                        }
                        else
                        {
                            var getGownSizes = await _gownSizeManager.GetAllByNonDeletedAsync();
                            if (getGownSizes.ResultStatus == ResultStatus.SUCCESS)
                                ViewBag.GownSizes = new SelectList(getGownSizes.Datas, "Size", "Size");
                            else
                                return await Task.Run(() => View("Error"));

                            return await Task.Run(() => View(new HomeIndexResultModel
                            {
                                studentGetterDto = new StudentGetterDto
                                {
                                    Name = student.Name,
                                    Surname = student.Surname,
                                    Faculty = getFaculty.Data.Name,
                                    Unit = getUnit.Data.Name,
                                    Program = getProgram.Data.Name
                                }
                            }));
                        }
                    }
                    return await Task.Run(() => View("Error"));
                }
                return await Task.Run(() => View("Error"));
            }
            return await Task.Run(() => View("Error"));
        }
        return await Task.Run(() => View("Error"));
    }

    public async Task<IActionResult> UpdateApply(int id = 5)
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        var getStudent = await _studentManager.GetByIdAsync(id, s => s.Companions);
        if (getStudent.ResultStatus == ResultStatus.SUCCESS)
        {
            var student = getStudent.Data;
            var getProgram = await _programManager.GetByIdAsync(student.ProgramId);
            if (getProgram.ResultStatus == ResultStatus.SUCCESS)
            {
                var getUnit = await _unitManager.GetByIdAsync(getProgram.Data.UnitId);
                if (getUnit.ResultStatus == ResultStatus.SUCCESS)
                {
                    var getFaculty = await _facultyManager.GetByIdAsync(getUnit.Data.FacultyId);
                    if (getFaculty.ResultStatus == ResultStatus.SUCCESS)
                    {
                        if (student.DidPaid)
                            return await Task.Run(() => View("401"));

                        HomeIndexResultModel homeIndexResultModel = new HomeIndexResultModel
                        {
                            studentGetterDto = new StudentGetterDto
                            {
                                Name = student.Name,
                                Surname = student.Surname,
                                Faculty = getFaculty.Data.Name,
                                Unit = getUnit.Data.Name,
                                Program = getProgram.Data.Name
                            },

                            addStudentDto = new AddStudentDto
                            {
                                GownRequest = student.GownRequest,
                                GownSize = student.GownSize,
                                Note = student.Note
                            },
                            IsUpdate = true
                        };

                        var getGownSizes = await _gownSizeManager.GetAllByNonDeletedAsync();
                        if (getGownSizes.ResultStatus == ResultStatus.SUCCESS)
                            ViewBag.GownSizes = new SelectList(getGownSizes.Datas, "Size", "Size");
                        else
                            return await Task.Run(() => View("Error"));
                        return await Task.Run(() => View("Index", homeIndexResultModel));
                    }
                    return await Task.Run(() => View("Error"));
                }
                return await Task.Run(() => View("Error"));
            }
            return await Task.Run(() => View("Error"));
        }
        return await Task.Run(() => View("Error"));
    }

    public async Task<IActionResult> AddCompanion(Student student)
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        return await Task.Run(() => View("Index", student));
    }

    [HttpPost]
    public async Task<IActionResult> AddCompanion(AddCompanionDto addCompanionDto)
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        var getStudent = await _studentManager.GetByIdAsync(5);
        if (getStudent.ResultStatus == ResultStatus.SUCCESS)
        {
            if (ModelState.IsValid)
            {
                var student = getStudent.Data;
                Companion companion = new Companion
                {
                    Name = addCompanionDto.Name,
                    Surname = addCompanionDto.Surname,
                    HesCode = addCompanionDto.HesCode,
                    PhoneNumber = addCompanionDto.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    StudentId = student.Id
                };

                var addCompanion = await _companionManager.AddAsync(companion);
                if (addCompanion.ResultStatus == ResultStatus.SUCCESS)
                {
                    student.CompanionStatus = true;
                    await _studentManager.UpdateAsync(student);
                    return await Task.Run(() => RedirectToAction("Index"));
                }
                TempData["ErrorMessage"] = "İşlem sırasında bir hata meydana geldi!";
                return await Task.Run(() => View("Index"));
            }
            return await Task.Run(() => RedirectToAction("Index"));
        }
        return await Task.Run(() => View("Error"));
    }

    public async Task<IActionResult> DeleteCompanion(int id)
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        var getCompanion = await _companionManager.GetByIdAsync(id);
        if (getCompanion.ResultStatus == ResultStatus.SUCCESS)
        {
            var companion = getCompanion.Data;
            if (companion != null && companion.DidJoin == false)
            {
                var deleteCompanion = await _companionManager.DeleteAsync(companion.Id);
                if (deleteCompanion.ResultStatus == ResultStatus.SUCCESS)
                {
                    var getStudent = await _studentManager.GetByIdAsync(5, s => s.Companions);
                    if (getStudent.ResultStatus == ResultStatus.SUCCESS)
                    {
                        var student = getStudent.Data;
                        if (student.Companions.Where(c => c.IsDeleted == false).ToList().Count == 0)
                        {
                            student.CompanionStatus = false;
                            var updateStudent = await _studentManager.UpdateAsync(student);
                            if (updateStudent.ResultStatus != ResultStatus.SUCCESS)
                            {
                                TempData["ErrorMessage"] = $"Öğrencinin Refakatçi Durumu Güncellenirken Bir Hata İle Karşılaşıldı!";
                                return await Task.Run(() => View("Index"));
                            }
                        }
                        TempData["SuccessMessage"] = $"Refakatçi <b>{companion.Name} {companion.Surname}</b> Başarıyla Silindi";
                        return await Task.Run(() => RedirectToAction("Index"));
                    }
                    return await Task.Run(() => View("Error"));
                }
                TempData["ErrorMessage"] = $"Refakatçi <b>{companion.Name} {companion.Surname}</b> Silinirken Bir Hata İle Karşılaşıldı!";
                return await Task.Run(() => RedirectToAction("Index"));
            }
            TempData["ErrorMessage"] = $"Böyle bir refakatçi bulunamadı!";
            return await Task.Run(() => RedirectToAction("Index"));
        }
        return await Task.Run(() => View("Error"));
    }

    public async Task<IActionResult> DeleteApply(int id)
    {
        var getRegisterDates = await _datesManager.GetAllByNonDeletedAsync();
        if (getRegisterDates.ResultStatus != ResultStatus.SUCCESS)
            return await Task.Run(() => View("Error"));
        var registerDate = getRegisterDates.Datas.OrderByDescending(rd => rd.CreatedDate).FirstOrDefault();
        if (registerDate != null && (DateTime.Today < registerDate.RegisterBeginningDate || DateTime.Today > registerDate.RegisterEndingDate))
            return await Task.Run(() => View("OutOfDate"));

        var getStudent = await _studentManager.GetByIdAsync(id, s => s.Companions);
        if (getStudent.ResultStatus == ResultStatus.SUCCESS)
        {
            var student = getStudent.Data;
            if (student != null)
            {
                if (student.DidPaid == true && student.DidTake == false)
                {
                    TempData["ErrorCardMessage"] = "Başvurunuzun silinmesi için cübbe teslimi yapmanız gerekmektedir.";
                    return await Task.Run(() => View());
                }

                if (student.DidJoin == true)
                {
                    TempData["ErrorCardMessage"] = "Başvurunuz katılım gösterdikten sonra silinememektedir.";
                    return await Task.Run(() => View());
                }

                if (student != null && student.HaveApplied == true)
                {
                    student.GownRequest = false;
                    student.GownSize = null;
                    student.CompanionStatus = false;
                    student.Note = null;
                    student.ModifiedDate = DateTime.Now;
                    student.HaveApplied = false;
                    student.IsDeleted = true;
                    var updateStudent = await _studentManager.UpdateAsync(student);
                    if (updateStudent.ResultStatus == ResultStatus.SUCCESS)
                    {
                        if (student.Companions != null && student.Companions.Count > 0)
                        {
                            foreach (var companion in student.Companions)
                            {
                                var deleteCompanion = await _companionManager.DeleteAsync(companion.Id);
                                if (deleteCompanion.ResultStatus != ResultStatus.SUCCESS)
                                {
                                    TempData["ErrorMessage"] = "Başvurunuz silinirken bir hata meydana geldi. " + deleteCompanion.Exception.Message;
                                    return await Task.Run(() => View("Index"));
                                }
                            }
                        }
                        TempData["SuccessMessage"] = $"Başvurunuz {DateTime.Now.ToString("dd MMM yyyy")} tarihinde başarıyla silinmiştir.";
                        return await Task.Run(() => View());
                    }
                    TempData["ErrorCardMessage"] = "Başvurunuz silinirken bir hata meydana geldi.";
                    return await Task.Run(() => View("Index"));
                }
                TempData["ErrorCardMessage"] = "Herhangi bir başvurunuz bulunmamakta!";
                return await Task.Run(() => View());
            }
            TempData["ErrorMessage"] = "Böyle bir öğrenci bulunamadı!";
            return await Task.Run(() => View("Error"));
        }
        TempData["ErrorMessage"] = "Başvurunuz silinirken bir hata meydana geldi.";
        return await Task.Run(() => View("Error"));
    }

    public async Task<IActionResult> OutOfDate()
    {
        return await Task.Run(() => View());
    }

    public async Task<IActionResult> Error()
    {
        return await Task.Run(() => View());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task InvitationSenderAsync(string Name, string Surname)
    {
        var invitationPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app-assets", "images", "Invitation", "Invitation.png");
        var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app-assets", "fonts", "fsm", "BeauRivage.TTF");
        Image image = Image.Load(System.IO.File.ReadAllBytes(invitationPath)); // Create any way you like.

        FontCollection collection = new();
        FontFamily family = collection.Add(fontPath);
        Font font = family.CreateFont(80);

        string text = $"Sn. {Name} {Surname}";
        if (text.Length > 54)
        {
            var textLength = 80 - (text.Length - 54) * 1.1F;
            font = family.CreateFont(textLength);
        }

        // The options are optional
        TextOptions options = new(font)
        {

            Origin = new PointF(image.Width / 2, 430), // Set the rendering origin.
            TabWidth = 0, // A tab renders as 8 spaces wide
            WrappingLength = 1500, // Greater than zero so we will word wrap at 100 pixels wide
            HorizontalAlignment = HorizontalAlignment.Center // Right align
        };

        IBrush brush = Brushes.Vertical(Color.Black, Color.Black);
        IPen pen = Pens.DashDot(Color.Black, 1);

        // Draws the text with horizontal red and blue hatching with a dash dot pattern outline.
        image.Mutate(x => x.DrawText(options, text, brush, pen));
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "app-assets", "images", "Invitation", "asd.jpeg");
        await image.SaveAsJpegAsync(path);
        image.Dispose();
    }
}
