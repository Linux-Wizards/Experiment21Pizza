namespace Experiment21.Services;

using Experiment21.Models;
using Experiment21.Data;

public class OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void DeleteOrder(int id)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == id);
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
}
