using Microsoft.AspNetCore.Mvc;
using TableStore.Models;
using TableStore.Interfaces;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace TableStore.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IOrder _orders;
        private readonly ITable _tables;
        private readonly IEmployee _employees;
        private readonly IClient _clients;

        public StatisticsController(IOrder orders, ITable tables, IEmployee employees, IClient clients)
        {
            _orders = orders;
            _tables = tables;
            _employees = employees;
            _clients = clients;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var salesWorksheet = package.Workbook.Worksheets.Add("Продажи");
                var employeesWorksheet = package.Workbook.Worksheets.Add("Работники");
                var clientsWorksheet = package.Workbook.Worksheets.Add("Клиенты");

                salesWorksheet.Cells[1, 1].Value = "Товар";
                salesWorksheet.Cells[1, 2].Value = "Количество продаж";

                employeesWorksheet.Cells[1, 1].Value = "Код";
                employeesWorksheet.Cells[1, 2].Value = "Фамилия";
                employeesWorksheet.Cells[1, 3].Value = "Имя";
                employeesWorksheet.Cells[1, 4].Value = "Отчество";
                employeesWorksheet.Cells[1, 5].Value = "Количество одобренных заказов";

                clientsWorksheet.Cells[1, 1].Value = "Код";
                clientsWorksheet.Cells[1, 2].Value = "Фамилия";
                clientsWorksheet.Cells[1, 3].Value = "Имя";
                clientsWorksheet.Cells[1, 4].Value = "Отчество";
                clientsWorksheet.Cells[1, 5].Value = "Количество завершенных заказов";

                var tables = _tables.GetAllTables();
                var salesByTable = new Dictionary<string, int>();

                var orders = _orders.GetAllOrders().ToList();

                foreach (var table in tables)
                {
                    int salesCount = orders.Sum(o => o.Positions.Where(p => p.TableId == table.Id && o.Status == "Завершен").Sum(p => p.Count));
                    salesByTable[table.Model] = salesCount;
                }

                int row = 2;
                foreach (var kvp in salesByTable)
                {
                    salesWorksheet.Cells[row, 1].Value = kvp.Key;
                    salesWorksheet.Cells[row, 2].Value = kvp.Value;
                    row++;
                }

                var employees = _employees.GetAllEmployees();
                var salesByEmployee = new Dictionary<string, int>();

                foreach (var employee in employees)
                {
                    int approvedOrdersCount = orders
                        .Count(o => o.EmployeeId == employee.Id && o.Status == "Завершен");
                    salesByEmployee[employee.Id + " - " + employee.Surname + " - " + employee.Name + " - " + employee.Patronymic] = approvedOrdersCount;
                }

                row = 2;
                foreach (var kvp in salesByEmployee)
                {
                    var employeeInfo = kvp.Key.Split(" - ");
                    employeesWorksheet.Cells[row, 1].Value = employeeInfo[0];
                    employeesWorksheet.Cells[row, 2].Value = employeeInfo[1];
                    employeesWorksheet.Cells[row, 3].Value = employeeInfo[2];
                    employeesWorksheet.Cells[row, 4].Value = employeeInfo[3];
                    employeesWorksheet.Cells[row, 5].Value = kvp.Value;
                    row++;
                }

                var clients = _clients.GetAllClients();
                var salesByClient = new Dictionary<string, int>();

                foreach (var client in clients)
                {
                    int approvedOrdersCount = orders
                        .Count(o => o.ClientId == client.Id && o.Status == "Завершен");
                    salesByClient[client.Id + " - " + client.Surname + " - " + client.Name + " - " + client.Patronymic] = approvedOrdersCount;
                }

                row = 2;
                foreach (var kvp in salesByClient)
                {
                    var clientInfo = kvp.Key.Split(" - ");
                    clientsWorksheet.Cells[row, 1].Value = clientInfo[0];
                    clientsWorksheet.Cells[row, 2].Value = clientInfo[1];
                    clientsWorksheet.Cells[row, 3].Value = clientInfo[2];
                    clientsWorksheet.Cells[row, 4].Value = clientInfo[3];
                    clientsWorksheet.Cells[row, 5].Value = kvp.Value;
                    row++;
                }

                MemoryStream stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = "Отчет.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(stream, contentType, fileName);
            }
        }

    }
}
