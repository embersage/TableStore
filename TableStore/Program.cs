using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TableStore.Interfaces;
using TableStore.Models;
using TableStore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IProvider, ProviderRepository>();

builder.Services.AddTransient<IConsignment, ConsignmentRepository>();

builder.Services.AddTransient<ITable, TableRepository>();

builder.Services.AddTransient<IKitchenTable, KitchenTableRepository>();

builder.Services.AddTransient<IComputerDesk, ComputerDeskRepository>();

builder.Services.AddTransient<IClient, ClientRepository>();

builder.Services.AddTransient<IEmployee, EmployeeRepository>();

builder.Services.AddTransient<IFiredEmployee, FiredEmployeeRepository>();

builder.Services.AddTransient<IOrder, OrdersRepository>();


IConfigurationRoot _confString = new ConfigurationBuilder().
    SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
    options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("employee"));
    options.AddPolicy("ClientOnly", policy => policy.RequireRole("client"));
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "TableStore.Session";
    options.IdleTimeout = TimeSpan.FromHours(48);
    options.Cookie.HttpOnly = false;
});

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Tables/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
