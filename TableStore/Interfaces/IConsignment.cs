using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IConsignment
    {
        IEnumerable<Consignment> GetAllConsignments();
        void AddConsignment(Consignment consignment);
        Consignment GetConsignment(int id);
        void UpdateConsignment(Consignment consignment);
        void DeleteConsignment(Consignment consignment);
    }
}
