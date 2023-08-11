using Microsoft.EntityFrameworkCore;
using SSquaredApplication.Models;

namespace SSquaredApplication.DataAccessLayer
{
    public class SSEnterpriseContext : DbContext
    {
        public SSEnterpriseContext(DbContextOptions<SSEnterpriseContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<EmployeeRoles> EmployeeRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "Admin", LastName = "Admin", ReportingManagerEmployeeId = 0 }
        );

            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<Role>().HasData(
                new Role { Name = "Director", RoleId = 1 },
                new Role { Name = "IT Support", RoleId = 2 },
                new Role { Name = "Support", RoleId = 3 },
                new Role { Name = "Accounting", RoleId = 4 },
                new Role { Name = "Sales", RoleId = 5 },
                new Role { Name = "Analyst", RoleId = 6 },
                new Role { Name = "IT Sales", RoleId = 7 }
        );

            modelBuilder.Entity<EmployeeRoles>().ToTable("EmployeeRoles");
            modelBuilder.Entity<EmployeeRoles>().HasData(
                new EmployeeRoles { Id = 1, RoleId = 1, EmployeeId = 1 }
        );
        }
    }
}
