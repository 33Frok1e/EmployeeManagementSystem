using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                   //new Employee
                   //{
                   //    Id = 1,
                   //    Name = "Elina",
                   //    Email = "elina@gmail.com",
                   //    Department = Dept.IT,
                   //    PhotoPath = "~/Images/image1.jpg"
                   //},
                   //new Employee
                   //{
                   //    Id = 2,
                   //    Name = "Krishna",
                   //    Email = "krishna@gmail.com",
                   //    Department = Dept.HR,
                   //    PhotoPath = "~/Images/image1.jpg"
                   //}
               );
        }
    }
}
