using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace ConsignmentStore.Controllers
{
    public class ConsignmentsController : Controller
    {
        private readonly IConsignment _consignments;
        private readonly IProvider _providers;

        public ConsignmentsController(IConsignment consignments, IProvider providers)
        {
            _consignments = consignments;
            _providers = providers;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_consignments.GetAllConsignments());
        }

        [HttpGet]
        public IActionResult UpdateConsignment(int id)
        {
            ViewBag.Providers = _providers.GetAllProviders();
            return View(id == 0 ? new Consignment() : _consignments.GetConsignment(id));
        }

        [HttpPost]
        public IActionResult UpdateConsignment(Consignment consignment)
        {
            if (consignment.Id == 0)
            {
                _consignments.AddConsignment(consignment);
            }
            else
            {
                _consignments.UpdateConsignment(consignment);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteConsignment(Consignment consignment)
        {
            _consignments.DeleteConsignment(consignment);
            return RedirectToAction(nameof(Index));
        }
    }
}