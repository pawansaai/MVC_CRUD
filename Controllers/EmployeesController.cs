using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Domain;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCdemoDbContext mvcdemodbcontext;

        public EmployeesController(MVCdemoDbContext mvcdemodbcontext)
        {
            this.mvcdemodbcontext = mvcdemodbcontext;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
           var employees = await mvcdemodbcontext.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeNewModel AddEmployeeRequest) 
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = AddEmployeeRequest.Name,
                DOB = AddEmployeeRequest.DOB,
                Email = AddEmployeeRequest.Email,
                PhoneNumber = AddEmployeeRequest.PhoneNumber,
                Department = AddEmployeeRequest.Department,
                Salary = AddEmployeeRequest.Salary

            };

            await mvcdemodbcontext.Employees.AddAsync(employee);
            await mvcdemodbcontext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {

            var employee = await mvcdemodbcontext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    DOB = employee.DOB,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    Department = employee.Department,
                    Salary = employee.Salary
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index"); 

        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcdemodbcontext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.DOB = model.DOB;
                employee.Email = model.Email;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Department = model.Department;
                employee.Salary = model.Salary;

                await mvcdemodbcontext.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcdemodbcontext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcdemodbcontext.Employees.Remove(employee);

                await mvcdemodbcontext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
             
        }

    }
}
