using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TableStore.Interfaces;
using TableStore.Models;

public class ClientOrdersController : Controller
{
    private readonly IOrder _orders;
    private readonly ITable _tables;

    public ClientOrdersController(IOrder orders, ITable tables)
    {
        _orders = orders;
        _tables = tables;
    }

    public IActionResult Index()
    {
        var userData = Request.Cookies["UserData"];
        var clientData = JsonConvert.DeserializeObject<Client>(userData);
        return View(_orders.GetAllOrders().Where(c => c.ClientId == clientData.Id).AsQueryable());
    }

    [HttpPost]
    public IActionResult CancelOrder(Order order)
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

    [HttpGet]
    public IActionResult GetOrderDetails(int id)
    {
        Order order = _orders.GetOrder(id);
        if (order != null)
        {
            foreach (Position position in order.Positions)
            {
                position.Table = _tables.GetTable(position.TableId);
            }
            return PartialView("OrderDetails", order);
        }
        else
        {
            return Content("Заказ не найден.");
        }
    }
}