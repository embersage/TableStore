using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _employees;
        private readonly IFiredEmployee _firedEmployees;

        public EmployeesController(IEmployee employees, IFiredEmployee firedEmployees)
        {
            _employees = employees;
            _firedEmployees = firedEmployees;
        }

        public IActionResult Index()
        {
            return View(_employees.GetAllEmployees());
        }

        [HttpGet]
        public IActionResult UpdateEmployee(int id)
        {
            return View(id == 0 ? new Employee() : _employees.GetEmployee(id));
        }

        [HttpPost]
        public IActionResult UpdateEmployee(Employee employee)
        {
            if (employee.Id == 0)
            {
                _employees.AddEmployee(employee);
            }
            else
            {
                _employees.UpdateEmployee(employee);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteEmployee(Employee employee)
        {
            try
            {
                Employee currentEmployee = _employees.GetEmployee(employee.Id);
                FiredEmployee firedEmployee = new FiredEmployee
                {
                    Surname = currentEmployee.Surname,
                    Name = currentEmployee.Name,
                    Patronymic = currentEmployee.Patronymic,
                    Gender = currentEmployee.Gender,
                    ContactNumber = currentEmployee.ContactNumber,
                    BirthDate = currentEmployee.BirthDate,
                    FiredFrom = DateTime.Today
                };
                _employees.DeleteEmployee(currentEmployee);
                _firedEmployees.AddFiredEmployee(firedEmployee);
            }
            catch {

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
