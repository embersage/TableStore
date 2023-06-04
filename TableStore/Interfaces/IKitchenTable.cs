using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IKitchenTable
    {
        IEnumerable<KitchenTable> GetAllKitchenTables();
        void AddKitchenTable(KitchenTable kitchenTable);
        KitchenTable GetKitchenTable(int id);
        void UpdateKitchenTable(KitchenTable kitchenTable);
        void DeleteKitchenTable(KitchenTable kitchenTable);
    }
}