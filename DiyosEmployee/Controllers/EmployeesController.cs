using DiyosEmployee.Data;
using DiyosEmployee.Models;
using Microsoft.AspNetCore.Mvc;
using DiyosEmployee.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DiyosEmployee.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DiyosEmployeeDbContext diyosDBContext;

        public EmployeesController(DiyosEmployeeDbContext diyosDBContext)
        {
            this.diyosDBContext = diyosDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await diyosDBContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeModel addEmployeeRequest, IFormFile photoFile)
        {
            var employee = new EmployeeModel()
            {
                Id = Guid.NewGuid(),
                EmpId = addEmployeeRequest.EmpId,
                EmpName = addEmployeeRequest.EmpName,
                Gender = addEmployeeRequest.Gender,
                DOB = addEmployeeRequest.DOB,
                Place = addEmployeeRequest.Place,
            };

            if (photoFile != null && photoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photoFile.CopyToAsync(memoryStream);
                    employee.Photo = memoryStream.ToArray();
                }
            }

            await diyosDBContext.Employees.AddAsync(employee);
            await diyosDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int empId)
        {
            var employee = await diyosDBContext.Employees.FirstOrDefaultAsync(x => x.EmpId == empId);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    EmpId = employee.EmpId,
                    EmpName = employee.EmpName,
                    Gender = employee.Gender,
                    DOB = employee.DOB,
                    Place = employee.Place,
                };
                return await Task.Run(() => View("View",viewModel)); // Return the view with the employee details.
            }

            return RedirectToAction("Index"); // If no employee is found, redirect to the index page.
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateEmployeeViewModel model)
        {
            var employee = await diyosDBContext.Employees.FirstOrDefaultAsync(x => x.EmpId == model.EmpId);
            if (employee != null)
            {
                employee.EmpName = model.EmpName;
                employee.Gender = model.Gender;
                employee.DOB = model.DOB;
                employee.Place = model.Place;
                await diyosDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await diyosDBContext.Employees.FirstOrDefaultAsync(x => x.EmpId == model.EmpId);
            if (employee != null)
            {
                diyosDBContext.Employees.Remove(employee);
                await diyosDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
