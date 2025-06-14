using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Services;

public class IndexModel : PageModel
{
    private readonly WorkshopContext _context;

    public IndexModel(WorkshopContext context)
    {
        _context = context;
    }

    public IList<Service> ServiceList { get; set; } = new List<Service>();

    public async Task OnGetAsync()
    {
        ServiceList = await _context.Services.ToListAsync();
    }
}