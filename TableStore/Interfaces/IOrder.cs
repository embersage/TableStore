using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IOrder
    {
        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);
        Order GetOrder(int id);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
