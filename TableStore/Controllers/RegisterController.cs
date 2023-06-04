using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationContext _dbContext;

        public RegisterController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Client client)
        {
            if (ModelState.IsValid)
            {
                var existingClient = await _dbContext.Clients.FirstOrDefaultAsync(u => u.Login == client.Login);
				var existingEmployee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Login == client.Login);
				if (existingClient!= null || existingEmployee != null || client.Login == "admin")
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким логином уже зарегистрирован");
                    return View(client);
                }

                var newClient = new Client
                {
                    Surname = client.Surname,
                    Name = client.Name,
                    Patronymic = client.Patronymic,
                    Gender = client.Gender,
                    ContactNumber = client.ContactNumber,
                    BirthDate = client.BirthDate,
                    Login = client.Login,
                    Password = client.Password,
                };

                _dbContext.Clients.Add(newClient);
                await _dbContext.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Surname, client.Surname),
                    new Claim(ClaimTypes.Name, client.Name),
                    new Claim(ClaimTypes.Gender, client.Gender),
                    new Claim(ClaimTypes.DateOfBirth, client.BirthDate.ToString()),
                    new Claim(ClaimTypes.HomePhone, client.ContactNumber),
                    
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                var userData = JsonConvert.SerializeObject(newClient);
                HttpContext.Response.Cookies.Append("UserData", userData);

                return RedirectToAction("Index", "Store");
            }

            return View(client);
        }
    }
}
