using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoWorkshopWeb.Pages.Clients;

public class IndexModel : PageModel
{
    private readonly WorkshopContext _context;

    public IndexModel(WorkshopContext context)
    {
        _context = context;
    }

    public IList<Client> ClientList { get; set; } = new List<Client>();

    public async Task OnGetAsync()
    {
        ClientList = await _context.Clients.ToListAsync();
    }
}