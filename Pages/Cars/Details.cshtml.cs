using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Cars;

public class DetailsModel : PageModel
{
    private readonly WorkshopContext _context;

    public DetailsModel(WorkshopContext context)
    {
        _context = context;
    }

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
}