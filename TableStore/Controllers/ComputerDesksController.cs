using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class ComputerDesksController : Controller
    {
        private readonly IComputerDesk _computerDesks;

        public ComputerDesksController(IComputerDesk computerDesks)
        {
            _computerDesks = computerDesks;
        }
        public IActionResult Index()
        {
            return View(_computerDesks.GetAllComputerDesks());
        }
        [HttpPost]
        public IActionResult AddComputerDesk(ComputerDesk computerDesk)
        {
            _computerDesks.AddComputerDesk(computerDesk);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult UpdateComputerDesk(ComputerDesk computerDesk)
        {
            _computerDesks.UpdateComputerDesk(computerDesk);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteComputerDesk(ComputerDesk computerDesk)
        {
            _computerDesks.DeleteComputerDesk(computerDesk);
            return RedirectToAction(nameof(Index));
        }
    }
}
