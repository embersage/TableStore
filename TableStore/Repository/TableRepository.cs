using Microsoft.EntityFrameworkCore;
using TableStore.Interfaces;
using TableStore.Models;

public class TableRepository : ITable
{
    private ApplicationContext _context;

    public TableRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void AddTable(Table table)
    {
		_context.Tables.Add(table);
	    _context.SaveChanges();
    }

    public IEnumerable<Table> GetAllTables()
    {
        return _context.Tables;
    }

    public Table GetTable(int id)
    {
        return _context.Tables.Find(id);
    }

    public void UpdateTable(Table table)
    {
        Table table2 = GetTable(table.Id);
        table2.ConsignmentId = table.ConsignmentId;
        table2.Count = table.Count;
        table2.Manufacturer = table.Manufacturer;
        table2.Model = table.Model;
        table2.LifeTime = table.LifeTime;
        table2.Guarantee = table.Guarantee;
        table2.Weight = table.Weight;
        table2.Width = table.Width;
        table2.Height = table.Height;
        table2.Depth = table.Depth;
        table2.CountertopMaterial = table.CountertopMaterial;
        table2.UnderframeMaterial = table.UnderframeMaterial;
        table2.CountertopColor = table.CountertopColor;
        table2.UnderframeColor = table.UnderframeColor;
        table2.CountertopType = table.CountertopType;
        table2.MaxLoad = table.MaxLoad;
        //table2.Type = table.Type;
        table2.Price = table.Price;
        _context.SaveChanges();
    }

    public void DeleteTable(Table table)
    {
        _context.Tables.Remove(table);
        _context.SaveChanges();
    }
}
