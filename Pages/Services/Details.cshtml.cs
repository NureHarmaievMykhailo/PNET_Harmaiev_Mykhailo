using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoWorkshopWeb.Pages.Services;

public class DetailsModel : PageModel
{
    private readonly WorkshopContext _context;

    public DetailsModel(WorkshopContext context)
    {
        _context = context;
    }

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
}