using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TableStore.Interfaces;
using TableStore.Models;

public class OrdersController : Controller
{
    private readonly IOrder _orders;
    private readonly ITable _tables;

    public OrdersController(IOrder orders, ITable tables)
    {
        _orders = orders;
        _tables = tables;
    }

    public IActionResult Index()
    {
        return View(_orders.GetAllOrders());
    }

    //public IActionResult EditOrder(int id)
    //{
    //    var tables = _tables.GetAllTables();
    //    Order order = id == 0 ? new Order() : _orders.GetOrder(id);
    //    IDictionary<int, Position> positions = order.Positions?.ToDictionary(e => e.TableId) ?? new Dictionary<int, Position>();
    //    ViewBag.Positions = tables.Select(e => positions.ContainsKey(e.Id) ? positions[e.Id] : new Position
    //    {
    //        Table = e,
    //        TableId = e.Id,
    //        Count = 0
    //    });
    //    return View(order);
    //}

    [HttpPost]
    public IActionResult UpdateOrder(Order order)
    {
        order.Positions = order.Positions.Where(e => e.Id > 0 || (e.Id == 0 && e.Count > 0)).ToArray();
        if (order.Id == 0)
        {
            _orders.AddOrder(order);
        }
        else
        {
            _orders.UpdateOrder(order);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult DeleteOrder(Order order)
    {
        Order currentOrder = _orders.GetOrder(order.Id);
        foreach (Position position in currentOrder.Positions)
        {
            Table table = _tables.GetTable(position.TableId);
            table.Count += position.Count;
            _tables.UpdateTable(table);
        }
        _orders.DeleteOrder(currentOrder);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult FinishOrder(Order order)
    {
        var userData = Request.Cookies["UserData"];
        var employeeData = JsonConvert.DeserializeObject<Employee>(userData);
        Order currentOrder = _orders.GetOrder(order.Id);
        currentOrder.Status = "Завершен";
        currentOrder.EmployeeId = employeeData.Id;
        _orders.UpdateOrder(currentOrder);
        return RedirectToAction(nameof(Index));
    }
}