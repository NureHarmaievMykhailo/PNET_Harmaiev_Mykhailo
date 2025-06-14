using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoWorkshopWeb.Pages.Services;

public class DeleteModel : PageModel
{
    private readonly WorkshopContext _context;

    public DeleteModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service? Service { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Service = await _context.Services.FindAsync(id);

        if (Service == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var service = await _context.Services.FindAsync(id);

        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}