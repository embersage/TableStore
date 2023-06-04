using TableStore.Interfaces;
using TableStore.Models;

namespace TableStore.Repository
{
    public class ProviderRepository : IProvider
    {
        private ApplicationContext _context;

        public ProviderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Provider> GetAllProviders()
        {
            return _context.Providers;
        }

        public void AddProvider(Provider provider)
        {
            _context.Providers.Add(provider);
            _context.SaveChanges();
        }

        public Provider GetProvider(int id)
        {
            return _context.Providers.Find(id);
        }

        public void UpdateProvider(Provider provider)
        {
            _context.Providers.Update(provider);
            _context.SaveChanges();
        }

        public void DeleteProvider(Provider provider)
        {
            _context.Providers.Remove(provider);
            _context.SaveChanges();
        }
    }
}
