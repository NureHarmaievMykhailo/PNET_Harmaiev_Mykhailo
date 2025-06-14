using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Cars;

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

        var clientToUpdate = await _context.Clients.FirstOrDefaultAsync(c => c.ClientId == Client.ClientId);

        if (clientToUpdate == null)
            return NotFound();

        // Оновлюємо поля вручну
        clientToUpdate.FullName = Client.FullName;
        clientToUpdate.Phone = Client.Phone;
        clientToUpdate.Email = Client.Email;

        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}