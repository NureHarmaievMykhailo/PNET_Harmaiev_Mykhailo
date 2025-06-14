using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Clients;

public class EditModel : PageModel
{
    private readonly WorkshopContext _context;

    public EditModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == id);
        return Client == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        try
        {
            _context.Attach(Client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Clients.Any(c => c.ClientId == Client.ClientId))
                return NotFound();
            else
                throw;
        }

        return RedirectToPage("Index");
    }
}