using TableStore.Models;

namespace TableStore.Interfaces
{
    public interface IProvider
    {
        IEnumerable<Provider> GetAllProviders();
        void AddProvider(Provider provider);
        Provider GetProvider(int id);
        void UpdateProvider(Provider provider);
        void DeleteProvider(Provider provider);
    }
}
