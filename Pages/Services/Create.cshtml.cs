using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace AutoWorkshopWeb.Pages.Services;

public class CreateModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly ILogger<CreateModel> _logger;
    private readonly IMemoryCache _cache;

    public CreateModel(WorkshopContext context, ILogger<CreateModel> logger, IMemoryCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Services.Add(Service);
        await _context.SaveChangesAsync();

        // Додати логування в БД
        _context.ServiceLogs.Add(new ServiceLog
        {
            ServiceId = Service.ServiceId,
            OperationType = "Create",
            Message = $"Створено послугу: {Service.Name}",
            LogDate = DateTime.Now
        });
        await _context.SaveChangesAsync();

        _cache.Remove("services_list");

        _logger.LogInformation("Створено нову послугу: {Name} (ID: {Id})", Service.Name, Service.ServiceId);

        return RedirectToPage("Index");
    }
}