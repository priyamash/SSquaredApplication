using Microsoft.AspNetCore.Mvc;
using SSquaredApplication.Models;
using SSquaredApplication.Processor;

namespace SSquaredApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeProcessor _empProcessor;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeProcessor empProcessor)
        {

            _logger = logger;
            _empProcessor = empProcessor;
        }
        [BindProperty]
        public int[] SelectedRoles { get; set; }
        public async Task<IActionResult> Index()
        {
            var employeesResultModel = await _empProcessor.PopulateEmployeesResultModel();
            if (employeesResultModel?.Managers?.Count > 0)
            {
                employeesResultModel.HasManagers = true;
            }
            return View(employeesResultModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmployeesResultModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new ErrorViewModel { Message = "Invalid EmployeesResultModel" });
            }
            var selectedManager = model.SelectedManager;
            model = await _empProcessor.PopulateEmployeesResultModel(selectedManager);
            model.SelectedManager = selectedManager;
            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            var cem = new CreateEmployeeModel();
            cem.Managers = await _empProcessor.GetManagers();
            cem.Roles = await _empProcessor.GetRoles();
            return View(cem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] CreateEmployeeModel createEmployeeModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Error", new ErrorViewModel { Message = "Invalid CreateEmployeeModel" });
            }

            try
            {
                var employee = new Employee();
                employee.FirstName = createEmployeeModel.FirstName;
                employee.LastName = createEmployeeModel.LastName;
                employee.ReportingManagerEmployeeId = createEmployeeModel.SelectedManager;
                employee.EmployeeRoles = SelectedRoles?.Select(x => new EmployeeRoles { EmployeeId = createEmployeeModel.EmployeeId, RoleId = x }).ToList();
                var res = await _empProcessor.CreateEmployee(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Execption occured get data: {ex}");
                return Error();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { Message = "Some Error occured." });
        }

    }
}