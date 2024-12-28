using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [   
            Required,
            MaxLength(50, ErrorMessage = "Name can't exceed 50 characters"),
            MinLength(3, ErrorMessage = "Name is too short")
        ]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Office Mail")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        public string? Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
