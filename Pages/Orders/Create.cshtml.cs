using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AutoWorkshopWeb.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(WorkshopContext context, ILogger<CreateModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public Order Order { get; set; } = default!;

    public SelectList Cars { get; set; } = default!;
    public SelectList Services { get; set; } = default!;
    public SelectList Statuses { get; set; } = default!;

    public void OnGet()
    {
        Cars = new SelectList(_context.Cars.Include(c => c.Client), "CarId", "LicensePlate");
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

        // 👉 Запис у таблицю ServiceLogs
        _context.ServiceLogs.Add(new ServiceLog
        {
            ServiceId = Order.ServiceId,
            OperationType = "Create",
            Message = $"Створено замовлення для авто ID={Order.CarId}, послуга ID={Order.ServiceId}, статус: {Order.Status}",
            LogDate = DateTime.Now
        });

        await _context.SaveChangesAsync();

        // 👉 Лог у консоль
        _logger.LogInformation("Створено замовлення ID={OrderId}, Авто ID={CarId}, Послуга ID={ServiceId}, Статус: {Status}",
            Order.OrderId, Order.CarId, Order.ServiceId, Order.Status);

        return RedirectToPage("Index");
    }
}
