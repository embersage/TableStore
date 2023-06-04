using Microsoft.EntityFrameworkCore;
using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Repository
{
    public class OrdersRepository : IOrder
    {
        private ApplicationContext _context;

        public OrdersRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(e => e.Positions).ThenInclude(e => e.Table);
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Include(e => e.Positions).FirstOrDefault(e => e.Id == id);
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
