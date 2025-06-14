using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Services;

public class EditModel : PageModel
{
    private readonly WorkshopContext _context;

    public EditModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

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

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Service).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Services.Any(e => e.ServiceId == Service.ServiceId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("Index");
    }
}