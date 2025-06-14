using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoWorkshopWeb.Pages.Cars;

public class CreateModel : PageModel
{
    private readonly WorkshopContext _context;

    public CreateModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Car Car { get; set; } = default!;

    public SelectList Clients { get; set; } = default!;

    public void OnGet()
    {
        Clients = new SelectList(_context.Clients, "ClientId", "FullName");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Clients = new SelectList(_context.Clients, "ClientId", "FullName");
            return Page();
        }

        _context.Cars.Add(Car);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}