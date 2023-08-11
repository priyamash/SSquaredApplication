using Microsoft.EntityFrameworkCore;
using SSquaredApplication.Models;

namespace SSquaredApplication.DataAccessLayer
{
    public interface ISSEnterpriseContext
    {
        DbSet<Employee> Employee { get; set; }

        DbSet<Role> Role { get; set; }

        DbSet<EmployeeRoles> EmployeeRoles { get; set; }
    }
}
