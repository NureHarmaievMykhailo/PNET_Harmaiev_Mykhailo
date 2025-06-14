using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Orders;

public class IndexModel : PageModel
{
    private readonly WorkshopContext _context;

    public IndexModel(WorkshopContext context)
    {
        _context = context;
    }

    public IList<Order> OrderList { get; set; } = [];

    public async Task OnGetAsync()
    {
        OrderList = await _context.Orders
            .Include(o => o.Car)
            .ThenInclude(c => c.Client)
            .Include(o => o.Service)
            .ToListAsync();
    }
}