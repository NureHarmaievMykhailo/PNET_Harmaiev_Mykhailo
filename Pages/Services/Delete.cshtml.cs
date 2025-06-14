using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace AutoWorkshopWeb.Pages.Services;

public class DeleteModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly ILogger<DeleteModel> _logger;
    private readonly IMemoryCache _cache;

    public DeleteModel(WorkshopContext context, ILogger<DeleteModel> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    [BindProperty]
    public Service? Service { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Service = await _context.Services.FindAsync(id);

        return Service == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var service = await _context.Services.FindAsync(id);

        if (service != null)
        {
            _context.Services.Remove(service);

            _context.ServiceLogs.Add(new ServiceLog
            {
                ServiceId = service.ServiceId,
                OperationType = "Delete",
                Message = $"Видалено послугу: {service.Name}",
                LogDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            _cache.Remove("services_list");

            _logger.LogWarning("Послугу видалено: {Name} (ID: {Id})", service.Name, service.ServiceId);
        }

        return RedirectToPage("Index");
    }
}