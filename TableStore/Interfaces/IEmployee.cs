using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        Employee GetEmployee(int id);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
