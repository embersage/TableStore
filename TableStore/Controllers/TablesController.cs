using Microsoft.AspNetCore.Mvc;
using TableStore.Models;
using TableStore.Interfaces;

namespace TableStore.Controllers
{
    public class TablesController : Controller
    {
        private readonly ITable _tables;
        private readonly IConsignment _consignments;
        private readonly IComputerDesk _computerDesks;
        private readonly IKitchenTable _kitchenTables;
        

        public TablesController(ITable tables, IConsignment consignments, IComputerDesk computerDesks, IKitchenTable kitchenTables)
        {
            _tables = tables;
            _consignments = consignments;
            _computerDesks = computerDesks;
            _kitchenTables = kitchenTables;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_tables.GetAllTables());
        }

        [HttpGet]
        public IActionResult UpdateTable(int id)
        {
            ViewBag.Consignments = _consignments.GetAllConsignments();
            ViewBag.ComputerDesks = _computerDesks.GetAllComputerDesks();
            ViewBag.KitchenTables = _kitchenTables.GetAllKitchenTables();
            return View(id == 0 ? new Table() : _tables.GetTable(id));
        }

        [HttpPost]
        public IActionResult UpdateTable(Table table, bool heightAdjustable, string additionalOptions, int numberOfSeats, bool isExtendable)
        {
            if (table.Id == 0)
            {
                if (table.CountertopColor != "Выберите цвет столешницы" && table.UnderframeColor != "Выберите цвет подстолья" 
                    && table.CountertopType != "Выберите тип столешницы" && table.CountertopMaterial != "Выберите материал столешницы" 
                    && table.UnderframeMaterial != "Выберите материал подстолья" && table.Type != "Выберите тип стола"
                    && table.Count >= 0 && table.LifeTime > 0 && table.Guarantee > 0 && table.ConsignmentId > 0 
                    && table.Weight > 0 && table.Width > 0 && table.Height > 0 && table.Depth > 0 
                    && table.MaxLoad > 0 && table.Price > 0 && table.Model.Length > 0 
                    && table.Manufacturer.Length > 0)
                {
					_tables.AddTable(table);
                    if (table.Type == "Компьютерный" && additionalOptions.Length >= 3)
                    {
                        ComputerDesk computerDesk = new ComputerDesk { Id = table.Id, HeightAdjustable = heightAdjustable, AdditionalOptions = additionalOptions };
						_computerDesks.AddComputerDesk(computerDesk);
					}
					else if (table.Type == "Кухонный" && numberOfSeats > 0)
					{
						KitchenTable kitchenTable = new KitchenTable { Id = table.Id, NumberOfSeats = numberOfSeats, IsExtendable = isExtendable };
						_kitchenTables.AddKitchenTable(kitchenTable);
					}
				}
            }
            else
            {
                if (table.CountertopColor != "Выберите цвет столешницы" && table.UnderframeColor != "Выберите цвет подстолья"
                    && table.CountertopType != "Выберите тип столешницы" && table.CountertopMaterial != "Выберите материал столешницы"
                    && table.UnderframeMaterial != "Выберите материал подстолья" && table.Type != "Выберите тип стола"
                    && table.Count >= 0 && table.LifeTime > 0 && table.Guarantee > 0 && table.ConsignmentId > 0
                    && table.Weight > 0 && table.Width > 0 && table.Height > 0 && table.Depth > 0
                    && table.MaxLoad > 0 && table.Price > 0 && table.Model.Length > 0
                    && table.Manufacturer.Length > 0)
                {
                    if (table.Type == "Компьютерный" && _computerDesks.GetComputerDesk(table.Id) != null && additionalOptions.Length >= 3)
                    {
                        ComputerDesk computerDesk = _computerDesks.GetComputerDesk(table.Id);
                        computerDesk.HeightAdjustable = heightAdjustable;
                        computerDesk.AdditionalOptions = additionalOptions;
						_computerDesks.UpdateComputerDesk(computerDesk);
                    }
                    else if (table.Type == "Кухонный" && _kitchenTables.GetKitchenTable(table.Id) != null && numberOfSeats > 0)
                    {
						KitchenTable kitchenTable = _kitchenTables.GetKitchenTable(table.Id);
						kitchenTable.NumberOfSeats = numberOfSeats;
						kitchenTable.IsExtendable = isExtendable;
						_kitchenTables.UpdateKitchenTable(kitchenTable);
					}
					_tables.UpdateTable(table);
				}
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteTable(Table table)
        {
            _tables.DeleteTable(table);
            return RedirectToAction(nameof(Index));
        }

		[HttpGet]
		public IActionResult GetAdditionalInfo(int id)
		{
			Table table = _tables.GetTable(id);
			if (table != null)
			{
				ViewBag.ComputerDesk = _computerDesks.GetComputerDesk(id);
				ViewBag.KitchenTable = _kitchenTables.GetKitchenTable(id);
				return PartialView("AdditionalInfo", table);
			}
			else
			{
				return Content("Стол не найден.");
			}
		}
	}
}
