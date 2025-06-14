using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Services;

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

            var log = new ServiceLog
            {
                ServiceId = Service.ServiceId,
                OperationType = "Updated",
                Message = $"Послуга оновлена: {Service.Name}, Ціна: {Service.Price:C}",
                LogDate = DateTime.Now
            };
            _context.ServiceLogs.Add(log);
            await _context.SaveChangesAsync();

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