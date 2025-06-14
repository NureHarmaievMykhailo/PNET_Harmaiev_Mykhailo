using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Orders;

public class EditModel : PageModel
{
    private readonly WorkshopContext _context;

    public EditModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order Order { get; set; } = default!;

    public SelectList Cars { get; set; } = default!;
    public SelectList Services { get; set; } = default!;
    public SelectList Statuses { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Order = await _context.Orders
            .Include(o => o.Car)
            .Include(o => o.Service)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (Order == null)
            return NotFound();

        Cars = new SelectList(_context.Cars.Include(c => c.Client), "CarId", "LicensePlate");
        Services = new SelectList(_context.Services, "ServiceId", "Name");
        Statuses = new SelectList(new[] { "Прийнято", "Виконується", "Завершено" });

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Cars = new SelectList(_context.Cars.Include(c => c.Client), "CarId", "LicensePlate");
            Services = new SelectList(_context.Services, "ServiceId", "Name");
            Statuses = new SelectList(new[] { "Прийнято", "Виконується", "Завершено" });
            return Page();
        }

        _context.Attach(Order).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}