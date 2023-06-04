using Microsoft.EntityFrameworkCore;

namespace TableStore.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {

        }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<KitchenTable> KitchenTables { get; set; }
        public DbSet<ComputerDesk> ComputerDesks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FiredEmployee> FiredEmployees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Order> Orders { get; set; }
        
    }
}
