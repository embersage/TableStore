using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;
using TableStore.Infrastructure;

namespace TableStore.Controllers
{
    [ViewComponent(Name = "Cart")]
    public class CartController : Controller
    {
        private readonly ITable _tables;
        private readonly IOrder _orders;

        public CartController(ITable tables, IOrder orders)
        {
            _tables = tables;
            _orders = orders;
        }

        private Cart GetCart() => HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();

        private void SaveCart(Cart cart) => HttpContext.Session.SetJson("Cart", cart);

        public IActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(GetCart());
        }

        [HttpPost]
        public IActionResult AddToCart(Table table, string returnUrl, int available)
        {
            SaveCart(GetCart().AddItem(table, 1, available));
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int tableId, string returnUrl)
        {
            SaveCart(GetCart().RemoveItem(tableId));
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        public IActionResult Completed()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            order.Positions = GetCart().Selections.Select(e => new Position
            {
                TableId = e.TableId,
                Count = e.Count,
                SellingPrice = e.SellingPrice
            }).ToArray();
            _orders.AddOrder(order);
            foreach (Position position in order.Positions)
            {
                Table updatedTable = _tables.GetTable(position.TableId);
                updatedTable.Count -= position.Count;
                _tables.UpdateTable(updatedTable);
            }
            SaveCart(new Cart());
            return RedirectToAction(nameof(Completed));
        }

        //public IViewComponentResult Invoke(ISession session)
        //{
        //    return new ViewViewComponentResult()
        //    {
        //        ViewData = new ViewDataDictionary<Cart>(ViewData, session.GetJson<Cart>("Cart"))
        //    };
        //}
    }
}
