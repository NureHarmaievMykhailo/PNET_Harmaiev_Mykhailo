using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AutoWorkshopWeb.Pages.Services;

public class EditModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly ILogger<EditModel> _logger;
    private readonly IMemoryCache _cache;

    public EditModel(WorkshopContext context, ILogger<EditModel> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        Service = await _context.Services.FirstOrDefaultAsync(m => m.ServiceId == id);

        return Service == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.Attach(Service).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();

            // Додати запис у таблицю логів
            var log = new ServiceLog
            {
                ServiceId = Service.ServiceId,
                OperationType = "Updated",
                Message = $"Послуга оновлена: {Service.Name}, Ціна: {Service.Price:C}",
                LogDate = DateTime.Now
            };
            _context.ServiceLogs.Add(log);
            await _context.SaveChangesAsync();

            _cache.Remove("services_list");

            _logger.LogInformation("[Service Edit] Послуга {ServiceId} оновлена.", Service.ServiceId);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Services.Any(e => e.ServiceId == Service.ServiceId))
                return NotFound();
            throw;
        }

        return RedirectToPage("Index");
    }
}
