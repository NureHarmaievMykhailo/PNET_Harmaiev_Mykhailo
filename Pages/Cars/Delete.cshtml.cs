using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Cars;

public class DeleteModel : PageModel
{
    private readonly WorkshopContext _context;

    public DeleteModel(WorkshopContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Car? Car { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Car = await _context.Cars
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.CarId == id);

        return Car == null ? NotFound() : Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var car = await _context.Cars.FindAsync(id);

        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("Index");
    }
}