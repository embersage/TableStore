using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TableStore.Models;

namespace TableStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationContext _dbContext;

        public LoginController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == "admin" && model.Password == "admin")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, "admin")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Tables");
                }

                var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Login == model.Username && e.Password == model.Password);
                if (employee != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("sub", employee.Id.ToString()),
                        new Claim(ClaimTypes.Surname, employee.Surname),
                        new Claim(ClaimTypes.Name, employee.Name),
                        new Claim(ClaimTypes.Gender, employee.Gender),
                        new Claim(ClaimTypes.DateOfBirth, employee.BirthDate.ToString()),
                        new Claim(ClaimTypes.HomePhone, employee.ContactNumber),
                        new Claim(ClaimTypes.Role, "employee")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    var userData = JsonConvert.SerializeObject(employee);
                    HttpContext.Response.Cookies.Append("UserData", userData);

                    return RedirectToAction("Index", "Clients");
                }

                var client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Login == model.Username && c.Password == model.Password);
                if (client != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("sub", client.Id.ToString()),
                        new Claim(ClaimTypes.Surname, client.Surname),
                        new Claim(ClaimTypes.Name, client.Name),
                        new Claim(ClaimTypes.Gender, client.Gender),
                        new Claim(ClaimTypes.DateOfBirth, client.BirthDate.ToString()),
                        new Claim(ClaimTypes.HomePhone, client.ContactNumber),
                        new Claim(ClaimTypes.Role, "client")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    var userData = JsonConvert.SerializeObject(client);
                    HttpContext.Response.Cookies.Append("UserData", userData);

                    return RedirectToAction("Index", "Store");
                }

                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль.");
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
