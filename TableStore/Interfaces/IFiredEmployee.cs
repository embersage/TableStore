using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IFiredEmployee
    {
        IEnumerable<FiredEmployee> GetAllFiredEmployees();
        void AddFiredEmployee(FiredEmployee firedEmployee);
        FiredEmployee GetFiredEmployee(int id);
        void UpdateFiredEmployee(FiredEmployee firedEmployee);
        void DeleteFiredEmployee(FiredEmployee firedEmployee);
    }
}
