using Microsoft.EntityFrameworkCore;
using TableStore.Interfaces;
using TableStore.Models;

public class KitchenTableRepository : IKitchenTable
{
    private ApplicationContext _context;

    public KitchenTableRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void AddKitchenTable(KitchenTable kitchenTable)
    {
		_context.KitchenTables.Add(kitchenTable);
	    _context.SaveChanges();
    }

    public IEnumerable<KitchenTable> GetAllKitchenTables()
    {
        return _context.KitchenTables;
    }

    public KitchenTable GetKitchenTable(int id)
    {
        return _context.KitchenTables.Find(id);
    }

    public void UpdateKitchenTable(KitchenTable kitchenTable)
    {
        _context.KitchenTables.Update(kitchenTable);
        _context.SaveChanges();
    }

    public void DeleteKitchenTable(KitchenTable kitchenTable)
    {
        _context.KitchenTables.Remove(kitchenTable);
        _context.SaveChanges();
    }
}