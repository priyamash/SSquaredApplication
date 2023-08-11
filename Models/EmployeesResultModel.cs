using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SSquaredApplication.Models
{
    public class EmployeesResultModel
    {
        public EmployeesResultModel()
        {
            HasManagers = Managers != null && Managers.Any();
        }
        public List<Employee> SearchResults { get; set; }
        public bool HasManagers { get; set; }


        public List<Role> Roles { get; set; }
        public List<SelectListItem> Managers { get; set; }

        [BindProperty]
        public int SelectedManager { get; set; }

        public List<EmployeeRoles> EmployeeRoles { get; set; }

    }
}
