using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class clientsController : Controller
    {
        private readonly IClient _clients;

        public clientsController(IClient clients)
        {
            _clients = clients;
        }

        public IActionResult Index()
        {
            return View(_clients.GetAllClients());
        }

        [HttpGet]
        public IActionResult UpdateClient(int id)
        {
            return View(id == 0 ? new Client() : _clients.GetClient(id));
        }

        [HttpPost]
        public IActionResult UpdateClient(Client client)
        {
            if (client.Id == 0)
            {
                _clients.AddClient(client);
            }
            else
            {
                _clients.UpdateClient(client);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteClient(Client client)
        {
            _clients.DeleteClient(client);
            return RedirectToAction(nameof(Index));
        }
    }
}
