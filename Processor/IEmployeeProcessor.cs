using Microsoft.AspNetCore.Mvc.Rendering;
using SSquaredApplication.Models;

namespace SSquaredApplication.Processor
{
    public interface IEmployeeProcessor
    {
        Task<EmployeesResultModel> PopulateEmployeesResultModel(int? selectedManager = null);
        Task<List<SelectListItem>> GetManagers();
        Task<List<SelectListItem>> GetRoles();

        Task<int> CreateEmployee(Employee employee);
    }
}
