using Microsoft.AspNetCore.Mvc;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly IProvider _providers;

        public ProvidersController(IProvider providers)
        {
            _providers = providers;
        }

        public IActionResult Index()
        {
            return View(_providers.GetAllProviders());
        }

        [HttpGet]
        public IActionResult UpdateProvider(int id)
        {
            return View(id == 0 ? new Provider() : _providers.GetProvider(id));
        }

        [HttpPost]
        public IActionResult UpdateProvider(Provider provider)
        {
            if (provider.Id == 0)
            {
                _providers.AddProvider(provider);
            }
            else
            {
                _providers.UpdateProvider(provider);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteProvider(Provider Provider)
        {
            _providers.DeleteProvider(Provider);
            return RedirectToAction(nameof(Index));
        }
    }
}
