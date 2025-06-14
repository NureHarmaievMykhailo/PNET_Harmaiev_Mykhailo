using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoWorkshopWeb.Pages.Orders;

public class EditModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly ILogger<EditModel> _logger;

    public EditModel(WorkshopContext context, ILogger<EditModel> logger)
    {
        _context = context;
        _logger = logger;
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

        // 👇 Додати логування у таблицю
        _context.ServiceLogs.Add(new ServiceLog
        {
            ServiceId = Order.ServiceId,
            OperationType = "Edit",
            Message = $"Оновлено замовлення ID={Order.OrderId}, статус: {Order.Status}",
            LogDate = DateTime.Now
        });

        await _context.SaveChangesAsync();

        _logger.LogInformation("Замовлення ID={OrderId} оновлено. Авто ID={CarId}, Послуга ID={ServiceId}, Статус={Status}",
            Order.OrderId, Order.CarId, Order.ServiceId, Order.Status);

        return RedirectToPage("Index");
    }
}
