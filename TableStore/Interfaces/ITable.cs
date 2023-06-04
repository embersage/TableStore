using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface ITable
    {
        IEnumerable<Table> GetAllTables();
        void AddTable(Table table);
        Table GetTable(int id);
        void UpdateTable(Table table);
        void DeleteTable(Table table);
    }
}