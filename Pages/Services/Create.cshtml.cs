using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoWorkshopWeb.Pages.Services;

public class CreateModel : PageModel
{
    private readonly WorkshopContext _context;

    public CreateModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Services.Add(Service);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}