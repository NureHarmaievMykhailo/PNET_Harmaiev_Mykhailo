using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoWorkshopWeb.Pages.Orders;

public class DeleteModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly ILogger<DeleteModel> _logger;

    public DeleteModel(WorkshopContext context, ILogger<DeleteModel> logger)
    {
        _context = context;
        _logger = logger;
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

            // 👇 Додати логування у таблицю
            _context.ServiceLogs.Add(new ServiceLog
            {
                ServiceId = order.ServiceId,
                OperationType = "Delete",
                Message = $"Видалено замовлення ID={order.OrderId}",
                LogDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            _logger.LogWarning("Замовлення ID={OrderId} було видалено", order.OrderId);
        }

        return RedirectToPage("Index");
    }
}