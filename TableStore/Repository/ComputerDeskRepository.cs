using Microsoft.EntityFrameworkCore;
using TableStore.Interfaces;
using TableStore.Models;

public class ComputerDeskRepository : IComputerDesk
{
    private ApplicationContext _context;

    public ComputerDeskRepository(ApplicationContext context)
    {
        _context = context;
    }

    public void AddComputerDesk(ComputerDesk computerDesk)
    {
		_context.ComputerDesks.Add(computerDesk);
	    _context.SaveChanges();
    }

    public IEnumerable<ComputerDesk> GetAllComputerDesks()
    {
        return _context.ComputerDesks;
    }

    public ComputerDesk GetComputerDesk(int id)
    {
        return _context.ComputerDesks.Find(id);
    }

    public void UpdateComputerDesk(ComputerDesk computerDesk)
    {
        _context.ComputerDesks.Update(computerDesk);
        _context.SaveChanges();
    }

    public void DeleteComputerDesk(ComputerDesk computerDesk)
    {
        _context.ComputerDesks.Remove(computerDesk);
        _context.SaveChanges();
    }
}