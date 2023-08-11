using Microsoft.EntityFrameworkCore;
using SSquaredApplication.Models;

namespace SSquaredApplication.DataAccessLayer
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly SSEnterpriseContext _context;
        public EmployeeDAL(SSEnterpriseContext context)
        {
            _context = context;
        }

        public EmployeesResultModel model { get; set; }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task<IEnumerable<EmployeeRoles>> GetEmployeeRoles()
        {
            return await _context.EmployeeRoles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<int> Create(Employee employee)
        {
            _context.Employee.Add(employee);
            return await _context.SaveChangesAsync();
        }


    }

    public interface IEmployeeDAL
    {
        Task<IEnumerable<EmployeeRoles>> GetEmployeeRoles();

        Task<IEnumerable<Employee>> GetEmployees();

        Task<IEnumerable<Role>> GetAllRole();

        Task<int> Create(Employee employee);


    }

}

