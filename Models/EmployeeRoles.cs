using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSquaredApplication.Models
{
    public class EmployeeRoles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
