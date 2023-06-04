using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IComputerDesk
    {
        IEnumerable<ComputerDesk> GetAllComputerDesks();
        void AddComputerDesk(ComputerDesk computerDesk);
        ComputerDesk GetComputerDesk(int id);
        void UpdateComputerDesk(ComputerDesk computerDesk);
        void DeleteComputerDesk(ComputerDesk computerDesk);
    }
}