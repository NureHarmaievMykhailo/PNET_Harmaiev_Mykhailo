using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoWorkshopWeb.Pages.Clients;

public class DeleteModel : PageModel
{
    private readonly WorkshopContext _context;

    public DeleteModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client? Client { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Client = await _context.Clients.FindAsync(id);
        return Client == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}