using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Entities.Dtos.StatsDto;
using ApplyGraduate.MVC.Models.ResultModels;
using ApplyGraduate.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplyGraduate.MVC.Controllers
{
    public class StatsController : Controller
    {
        private readonly IGenericService<Student> _studentManager;
        private readonly IGenericService<Companion> _companionManager;
        private readonly IGenericService<Faculty> _facultyManager;
        private readonly IGenericService<Unit> _unitManager;
        private readonly IGenericService<ApplyGraduate.Entities.Concrete.Program> _programManager;
        private readonly IEmailSenderService _emailSenderManager;
        public StatsController(IGenericService<Student> studentManager, IGenericService<Companion> companionManager, IGenericService<Faculty> facultyManager, IGenericService<Unit> unitManager, IGenericService<ApplyGraduate.Entities.Concrete.Program> programManager, IEmailSenderService emailSenderManager)
        {
            _studentManager = studentManager;
            _companionManager = companionManager;
            _facultyManager = facultyManager;
            _unitManager = unitManager;
            _programManager = programManager;
            _emailSenderManager = emailSenderManager;
        }

        public async Task<IActionResult> Index()
        {
            //await _emailSenderManager.MailSenderAsync();
            var studentsQuery = _studentManager.SetQuery().Include(s => s.Program).ThenInclude(p => p.Unit).ThenInclude(u => u.Faculty).Include(s => s.Companions);
            var getStudents = await _studentManager.GetAllAsync(studentsQuery);
            if (getStudents.ResultStatus == ResultStatus.SUCCESS)
            {
                var companionsQuery = _companionManager.SetQuery().Include(c => c.Student).ThenInclude(s => s.Program).ThenInclude(p => p.Unit).ThenInclude(u => u.Faculty);
                var getCompanions = await _companionManager.GetAllAsync(companionsQuery);
                if (getCompanions.ResultStatus == ResultStatus.SUCCESS)
                {
                    var facultiesQuery = _facultyManager.SetQuery().Include(f => f.Units).ThenInclude(u => u.Programs).ThenInclude(p => p.Students).ThenInclude(s => s.Companions);
                    var getFaculties = await _facultyManager.GetAllAsync(facultiesQuery);
                    if (getFaculties.ResultStatus == ResultStatus.SUCCESS)
                    {
                        var unitsQuery = _unitManager.SetQuery().Include(u => u.Programs).ThenInclude(p => p.Students).ThenInclude(s => s.Companions).Include(u => u.Faculty);
                        var getUnits = await _unitManager.GetAllAsync(unitsQuery);
                        if (getUnits.ResultStatus == ResultStatus.SUCCESS)
                        {
                            var programsQuery = _programManager.SetQuery().Include(p => p.Unit).ThenInclude(u => u.Faculty).Include(p => p.Students).ThenInclude(s => s.Companions);
                            var getPrograms = await _programManager.GetAllAsync(programsQuery);
                            if (getPrograms.ResultStatus == ResultStatus.SUCCESS)
                            {
                                return await Task.Run(() => View(new StatsGettersResultModel
                                {
                                    statsStudentsGetterDto = new StatsStudentsGetterDto
                                    {
                                        Students = getStudents.Datas
                                    },
                                    statsCompanionsGetterDto = new StatsCompanionsGetterDto
                                    {
                                        Companions = getCompanions.Datas
                                    },
                                    statFacultiesGetterDto = new StatFacultiesGetterDto
                                    {
                                        Faculties = getFaculties.Datas.Where(f => f.IsDeleted == false).OrderBy(f => f.Name).ToList()
                                    },
                                    statsUnitsGetterDto = new StatsUnitsGetterDto
                                    {
                                        Units = getUnits.Datas.Where(u => u.IsDeleted == false).OrderBy(u => u.Name).ToList()
                                    },
                                    statsProgramsGetterDto = new StatsProgramsGetterDto
                                    {
                                        Programs = getPrograms.Datas.Where(p => p.IsDeleted == false).OrderBy(p => p.Name).ToList()
                                    }
                                }));
                            }
                            return await Task.Run(() => RedirectToAction("Error", "Home"));
                        }
                        return await Task.Run(() => RedirectToAction("Error", "Home"));
                    }
                    return await Task.Run(() => RedirectToAction("Error", "Home"));
                }
                return await Task.Run(() => RedirectToAction("Error", "Home"));
            }
            return await Task.Run(() => RedirectToAction("Error", "Home"));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? facultySelect, int? unitSelect, int? programSelect, int? FacultySelectInput, int? UnitSelectInput, int? ProgramSelectInput)
        {
            var studentsQuery = _studentManager.SetQuery().Include(s => s.Program).ThenInclude(p => p.Unit).ThenInclude(u => u.Faculty).Include(s => s.Companions);
            var getStudents = await _studentManager.GetAllAsync(studentsQuery);
            if (getStudents.ResultStatus == ResultStatus.SUCCESS)
            {
                var companionsQuery = _companionManager.SetQuery().Include(c => c.Student).ThenInclude(s => s.Program).ThenInclude(p => p.Unit).ThenInclude(u => u.Faculty);
                var getCompanions = await _companionManager.GetAllAsync(companionsQuery);
                if (getCompanions.ResultStatus == ResultStatus.SUCCESS)
                {
                    var facultiesQuery = _facultyManager.SetQuery().Include(f => f.Units).ThenInclude(u => u.Programs).ThenInclude(p => p.Students).ThenInclude(s => s.Companions);
                    var getFaculties = await _facultyManager.GetAllAsync(facultiesQuery);
                    if (getFaculties.ResultStatus == ResultStatus.SUCCESS)
                    {
                        var unitsQuery = _unitManager.SetQuery().Include(u => u.Programs).ThenInclude(p => p.Students).ThenInclude(s => s.Companions).Include(u => u.Faculty);
                        var getUnits = await _unitManager.GetAllAsync(unitsQuery);
                        if (getUnits.ResultStatus == ResultStatus.SUCCESS)
                        {
                            var programsQuery = _programManager.SetQuery().Include(p => p.Unit).ThenInclude(u => u.Faculty).Include(p => p.Students).ThenInclude(s => s.Companions);
                            var getPrograms = await _programManager.GetAllAsync(programsQuery);
                            if (getPrograms.ResultStatus == ResultStatus.SUCCESS)
                            {
                                return await Task.Run(() => View(new StatsGettersResultModel
                                {
                                    statsStudentsGetterDto = new StatsStudentsGetterDto
                                    {
                                        Students = getStudents.Datas
                                    },
                                    statsCompanionsGetterDto = new StatsCompanionsGetterDto
                                    {
                                        Companions = getCompanions.Datas
                                    },
                                    statFacultiesGetterDto = new StatFacultiesGetterDto
                                    {
                                        Faculties = getFaculties.Datas.Where(f => f.IsDeleted == false).OrderBy(f => f.Name).ToList()
                                    },
                                    statsUnitsGetterDto = new StatsUnitsGetterDto
                                    {
                                        Units = getUnits.Datas.Where(u => u.IsDeleted == false).OrderBy(u => u.Name).ToList()
                                    },
                                    statsProgramsGetterDto = new StatsProgramsGetterDto
                                    {
                                        Programs = getPrograms.Datas.Where(p => p.IsDeleted == false).OrderBy(p => p.Name).ToList()
                                    },
                                    FacultySelect = facultySelect ?? (int)FacultySelectInput,
                                    UnitSelect = unitSelect ?? (int)UnitSelectInput,
                                    ProgramSelect = programSelect ?? (int)ProgramSelectInput
                                }));
                            }
                            return await Task.Run(() => RedirectToAction("Error", "Home"));
                        }
                        return await Task.Run(() => RedirectToAction("Error", "Home"));
                    }
                    return await Task.Run(() => RedirectToAction("Error", "Home"));
                }
                return await Task.Run(() => RedirectToAction("Error", "Home"));
            }
            return await Task.Run(() => RedirectToAction("Error", "Home"));
        }
    }
}