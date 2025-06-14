using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AutoWorkshopWeb.Pages.Services;

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
    public Service Service { get; set; } = default!;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Services.Add(Service);
        await _context.SaveChangesAsync();
        
        _context.ServiceLogs.Add(new ServiceLog
        {
            ServiceId = Service.ServiceId,
            OperationType = "Create",
            Message = $"Створено послугу: {Service.Name}"
        });

        await _context.SaveChangesAsync();

        _logger.LogInformation("Створено нову послугу: {Name} (ID: {Id})", Service.Name, Service.ServiceId);

        return RedirectToPage("Index");
    }
}