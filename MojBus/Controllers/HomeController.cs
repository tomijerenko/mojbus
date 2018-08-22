using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using MojBus.Data.Entities;
using MojBus.Extensions;
using MojBus.Models;

namespace MojBus.Controllers
{
    public class HomeController : Controller
    {
        MojBusContext _context;

        public HomeController(MojBusContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Gtfsstops> stops = await _context.GetStops().ToListAsync();

            return View(stops);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
