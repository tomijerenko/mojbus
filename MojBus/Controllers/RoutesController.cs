using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Helpers;
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
            return View(MojBusHelper.RouteData(_context, routeName, DateTime.Now));
        }
    }
}
