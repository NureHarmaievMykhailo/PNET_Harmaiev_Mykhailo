using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoWorkshopWeb.Pages.Clients;

public class DetailsModel : PageModel
{
    private readonly WorkshopContext _context;

    public DetailsModel(WorkshopContext context)
    {
        _context = context;
    }

    public Client? Client { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Client = await _context.Clients.FindAsync(id);

        if (Client == null)
        {
            return NotFound();
        }

        return Page();
    }
}