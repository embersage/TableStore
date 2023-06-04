using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;

namespace TableStore.Controllers
{
	public class StoreController : Controller
	{
		private readonly ITable _tables;

		public StoreController(ITable tables)
		{
			_tables = tables;
		}

		[HttpGet]
		public IActionResult Index()
		{
            return View(_tables.GetAllTables());
		}
	}
}
