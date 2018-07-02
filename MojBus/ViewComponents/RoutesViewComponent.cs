using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojBus.ViewComponents
{
    public class RoutesViewComponent : ViewComponent
    {
        MojBusContext _context;

        public RoutesViewComponent(MojBusContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<string> stops = await _context.Gtfsroutes.Select(x => x.RouteShortName).Distinct().ToListAsync();
            return View(stops);
        }
    }
}
