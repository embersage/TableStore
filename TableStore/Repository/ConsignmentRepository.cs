using Microsoft.EntityFrameworkCore;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Repository
{
    public class ConsignmentRepository : IConsignment
    {
        private ApplicationContext _context;

        public ConsignmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void AddConsignment(Consignment consignment)
        {
            _context.Consignments.Add(consignment);
            _context.SaveChanges();
        }

        public IEnumerable<Consignment> GetAllConsignments()
        {
            return _context.Consignments.Include(e => e.Provider);
        }

        public Consignment GetConsignment(int id)
        {
            return _context.Consignments.Include(e => e.Provider).FirstOrDefault(e => e.Id == id);
        }

        public void UpdateConsignment(Consignment consignment)
        {
            Consignment consignment2 = _context.Consignments.Find(consignment.Id);
            consignment2.ProviderId = consignment.ProviderId;
            //consignment2.Provider = consignment.Provider;
            consignment2.Count = consignment.Count;
            consignment2.PurchasePrice = consignment.PurchasePrice;
            consignment2.DateOfOperation = consignment.DateOfOperation;
            _context.SaveChanges();
        }

        public void DeleteConsignment(Consignment consignment)
        {
            _context.Consignments.Remove(consignment);
            _context.SaveChanges();
        }
    }
}
