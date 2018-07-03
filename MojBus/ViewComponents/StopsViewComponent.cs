using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojBus.ViewComponents
{
    public class StopsViewComponent : ViewComponent
    {
        MojBusContext _context;

        public StopsViewComponent(MojBusContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<string> stops = await _context.Gtfsstops.Select(x => x.StopName).Distinct().ToListAsync();

            return View(stops);
        }
    }
}
