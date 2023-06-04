using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class FiredEmployeesController : Controller
    {
        private readonly IFiredEmployee _firedEmployees;

        public FiredEmployeesController(IFiredEmployee firedEmployees)
        {
            _firedEmployees = firedEmployees;
        }
        public IActionResult Index()
        {
            return View(_firedEmployees.GetAllFiredEmployees());
        }
        [HttpPost]
        public IActionResult AddFiredEmployee(FiredEmployee FiredEmployee)
        {
            _firedEmployees.AddFiredEmployee(FiredEmployee);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult UpdateFiredEmployee(FiredEmployee FiredEmployee)
        {
            _firedEmployees.UpdateFiredEmployee(FiredEmployee);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteFiredEmployee(FiredEmployee FiredEmployee)
        {
            _firedEmployees.DeleteFiredEmployee(FiredEmployee);
            return RedirectToAction(nameof(Index));
        }
    }
}
