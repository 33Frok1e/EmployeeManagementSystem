using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeRepository employeeRepository, IHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
        }
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }
        public ViewResult Details(int? Id)
        {
            //throw new Exception("Error in Details View");
            //Employee employee = _employeeRepository.GetEmployee(Id.Value);
            //if(employee == null)
            //{
            //    Response.StatusCode = 404;
            //    return View("EmployeeNotFound", Id.Value);
            //}

            //DetailsViewModel detailsViewModel = new DetailsViewModel()
            //{
            //    Employee = employee,
            //    PageTitle = "Employee Details"
            //};
            //return View(detailsViewModel);
            
            Employee employee = _employeeRepository.GetEmployee(Id.Value);
            if(employee != null)
            {
                DetailsViewModel detailsViewModel = new DetailsViewModel()
                {
                    Employee = employee,
                    PageTitle = "Employee Details"
                };
                return View(detailsViewModel);
            } else
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", Id.Value);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EditViewModel editViewModel = new EditViewModel
            {
                ID = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.ID);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/Images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    string uniqueFileName = ProcessUploadFile(model);
                    employee.PhotoPath = uniqueFileName;
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        private string ProcessUploadFile(CreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot/Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create)) { 
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
