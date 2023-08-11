using Microsoft.AspNetCore.Mvc.Rendering;

namespace SSquaredApplication.Models
{
    public class CreateEmployeeModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int SelectedManager { get; set; }

        public int[] SelectedRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<SelectListItem> Managers { get; set; }
    }
}
