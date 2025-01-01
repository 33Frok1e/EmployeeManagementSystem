using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [DisplayName("Role")]
        public string RoleName { get; set; }
    }
}
