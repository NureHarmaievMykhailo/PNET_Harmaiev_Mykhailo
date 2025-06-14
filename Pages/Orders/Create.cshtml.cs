using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoWorkshopWeb.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly WorkshopContext _context;

    public CreateModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Order Order { get; set; } = default!;

    public SelectList Cars { get; set; } = default!;
    public SelectList Services { get; set; } = default!;
    public SelectList Statuses { get; set; } = default!;

    public void OnGet()
    {
        Cars = new SelectList(_context.Cars.Include(c => c.Client),
            "CarId", "LicensePlate");

        Services = new SelectList(_context.Services, "ServiceId", "Name");

        Statuses = new SelectList(new[] { "Прийнято", "Виконується", "Завершено" });
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            OnGet(); // Перегенеруємо списки
            return Page();
        }

        Order.OrderDate = DateTime.Now;

        _context.Orders.Add(Order);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}