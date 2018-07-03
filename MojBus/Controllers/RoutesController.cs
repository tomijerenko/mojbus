using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Extensions;
using System;

namespace MojBus.Controllers
{
    public class RoutesController : Controller
    {
        MojBusContext _context;

        public RoutesController(MojBusContext context)
        {
            _context = context;
        }

        public IActionResult Index(string routeName)
        {
            ViewData["RouteName"] = routeName;
            return View(_context.RouteData(routeName, DateTime.Now));
        }
    }
}
