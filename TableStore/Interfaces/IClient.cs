using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IClient
    {
        IEnumerable<Client> GetAllClients();
        void AddClient(Client client);
        Client GetClient(int id);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
