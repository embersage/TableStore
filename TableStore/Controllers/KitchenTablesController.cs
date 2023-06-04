using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class KitchenTablesController : Controller
    {
        private readonly IKitchenTable _kitchenTables;

        public KitchenTablesController(IKitchenTable kitchenTables)
        {
            _kitchenTables = kitchenTables;
        }
        public IActionResult Index()
        {
            return View(_kitchenTables.GetAllKitchenTables());
        }
        [HttpPost]
        public IActionResult AddKitchenTable(KitchenTable kitchenTable)
        {
            _kitchenTables.AddKitchenTable(kitchenTable);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult UpdateKitchenTable(KitchenTable kitchenTable)
        {
            _kitchenTables.UpdateKitchenTable(kitchenTable);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteKitchenTable(KitchenTable kitchenTable)
        {
            _kitchenTables.DeleteKitchenTable(kitchenTable);
            return RedirectToAction(nameof(Index));
        }
    }
}
