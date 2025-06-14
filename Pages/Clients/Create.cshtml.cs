using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoWorkshopWeb.Pages.Clients;

public class CreateModel : PageModel
{
    private readonly WorkshopContext _context;

    public CreateModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = default!;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _context.Clients.Add(Client);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}