using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Cars;

public class IndexModel : PageModel
{
    private readonly WorkshopContext _context;

    public IndexModel(WorkshopContext context)
    {
        _context = context;
    }

    public IList<Car> CarList { get; set; } = [];

    public async Task OnGetAsync()
    {
        CarList = await _context.Cars
            .Include(c => c.Client)
            .ToListAsync();
    }
}