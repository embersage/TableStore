using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Repository
{
    public class FiredEmployeeRepository : IFiredEmployee
    {
        private ApplicationContext _context;

        public FiredEmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<FiredEmployee> GetAllFiredEmployees()
        {
            return _context.FiredEmployees;
        }

        public void AddFiredEmployee(FiredEmployee firedEmployee)
        {
            _context.FiredEmployees.Add(firedEmployee);
            _context.SaveChanges();
        }

        public FiredEmployee GetFiredEmployee(int id)
        {
            return _context.FiredEmployees.Find(id);
        }

        public void UpdateFiredEmployee(FiredEmployee firedEmployee)
        {
            _context.FiredEmployees.Update(firedEmployee);
            _context.SaveChanges();
        }

        public void DeleteFiredEmployee(FiredEmployee firedEmployee)
        {
            _context.FiredEmployees.Remove(firedEmployee);
            _context.SaveChanges();
        }
    }
}
