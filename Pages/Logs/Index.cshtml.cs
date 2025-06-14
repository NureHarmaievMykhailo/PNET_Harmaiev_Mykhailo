using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Logs;

public class IndexModel : PageModel
{
    private readonly WorkshopContext _context;

    public IndexModel(WorkshopContext context)
    {
        _context = context;
    }

    public IList<ServiceLog> Logs { get; set; } = new List<ServiceLog>();

    [BindProperty(SupportsGet = true)]
    public string? OperationType { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? FromDate { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime? ToDate { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.ServiceLogs.AsQueryable();

        if (!string.IsNullOrEmpty(OperationType))
            query = query.Where(l => l.OperationType == OperationType);

        if (FromDate.HasValue)
            query = query.Where(l => l.LogDate >= FromDate.Value.Date);

        if (ToDate.HasValue)
            query = query.Where(l => l.LogDate <= ToDate.Value.Date.AddDays(1).AddSeconds(-1));

        Logs = await query
            .OrderByDescending(l => l.LogDate)
            .Include(l => l.Service)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostClearAsync()
    {
        var allLogs = await _context.ServiceLogs.ToListAsync();
        _context.ServiceLogs.RemoveRange(allLogs);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}