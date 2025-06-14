using AutoWorkshopWeb.Data;
using AutoWorkshopWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace AutoWorkshopWeb.Pages.Services;

public class IndexModel : PageModel
{
    private readonly WorkshopContext _context;
    private readonly IMemoryCache _cache;

    public IndexModel(WorkshopContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public IList<Service> ServiceList { get; set; } = [];

    public async Task OnGetAsync()
    {
        // Ключ кешу
        const string cacheKey = "services_list";

        if (!_cache.TryGetValue(cacheKey, out List<Service>? cachedServices))
        {

            cachedServices = await Task.FromResult(_context.Services.ToList());

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, cachedServices, cacheOptions);
        }

        ServiceList = cachedServices!;
    }
}