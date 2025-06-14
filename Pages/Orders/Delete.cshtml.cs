using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Orders;

public class DeleteModel : PageModel
{
    private readonly WorkshopContext _context;

    public DeleteModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order? Order { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Order = await _context.Orders
            .Include(o => o.Car)
            .ThenInclude(c => c.Client)
            .Include(o => o.Service)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        return Order == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var order = await _context.Orders.FindAsync(id);

        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}