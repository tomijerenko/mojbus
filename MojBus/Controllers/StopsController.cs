using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Helpers;
using System;

namespace MojBus.Controllers
{
    public class StopsController : Controller
    {
        MojBusContext _context;

        public StopsController(MojBusContext context)
        {
            _context = context;
        }

        public IActionResult Index(string stopName)
        {
            return View(MojBusHelper.StopTimesForStop(_context, stopName, DateTime.Now));
        }
    }
}