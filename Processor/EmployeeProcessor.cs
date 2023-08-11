using Microsoft.AspNetCore.Mvc.Rendering;
using SSquaredApplication.DataAccessLayer;
using SSquaredApplication.Models;

namespace SSquaredApplication.Processor
{
    public class EmployeeProcessor : IEmployeeProcessor
    {
        private readonly SSEnterpriseContext _context;
        private readonly IEmployeeDAL _eDAL;
        public EmployeeProcessor(SSEnterpriseContext context, IEmployeeDAL eDAL)
        {
            _context = context;
            _eDAL = eDAL;
        }
        public async Task<EmployeesResultModel> PopulateEmployeesResultModel(int? selectedManager = null)
        {
            var model = new EmployeesResultModel();
            var emp = (List<Employee>)await _eDAL.GetEmployees();
            var emp_roles = (List<EmployeeRoles>)await _eDAL.GetEmployeeRoles();
            var roles = (List<Role>)await _eDAL.GetAllRole();

            model.SearchResults = ((selectedManager == null || (selectedManager ?? 0) == 0
                ? emp
                : (from e in emp where (e.ReportingManagerEmployeeId == selectedManager) select e).ToList()));


            model.EmployeeRoles = emp_roles;

            model.Roles = roles;

            var roleId = model.Roles?.FirstOrDefault(r => r.Name.Equals("Director")).RoleId;

            model.Managers = (List<SelectListItem>)await GetManagers();

            model.HasManagers = model.Managers != null && model.Managers.Any() && model.Managers.Any();
            return model;

        }



        public async Task<List<SelectListItem>> GetManagers()
        {
            var employees = (List<Employee>)await _eDAL.GetEmployees();
            var employeeRoles = (List<EmployeeRoles>)await _eDAL.GetEmployeeRoles();
            var roles = (List<Role>)await _eDAL.GetAllRole();

            var result = (from r in employeeRoles
                          join e in employees on r.EmployeeId equals e.EmployeeId
                          where r.RoleId == roles.FirstOrDefault(r => r.Name.Equals("Director")).RoleId
                          select new SelectListItem
                          {
                              Text = $"{e.FirstName} {e.LastName}",
                              Value = e.EmployeeId.ToString(),
                          }).ToList();

            result.Insert(0, new SelectListItem { Text = "--Select Manager--", Value = "", Disabled = true });
            result.Insert(1, new SelectListItem { Text = "ALL", Value = "0" });

            return result;
        }

        public async Task<List<SelectListItem>> GetRoles()
        {
            var roles = (List<Role>)await _eDAL.GetAllRole();

            return (from role in roles
                    select new SelectListItem
                    {
                        Text = role.Name,
                        Value = role.RoleId.ToString()
                    }).ToList();
        }

        public async Task<int> CreateEmployee(Employee employee)
        {
            return await _eDAL.Create(employee);
        }
    }
}
