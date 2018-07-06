using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MojBus.Data;
using MojBus.Extensions;
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

        public IActionResult Index(string stopName, int directionId)
        {
            _context.Database.Migrate();
            ViewData["StopName"] = stopName;
            ViewData["DirectionId"] = directionId;
            return View(_context.StopTimesForStop(stopName, directionId, DateTime.Now));
        }

        public IActionResult StopDataForRoute(string stopName, string routeShortName, int directionId)
        {
            ViewData["StopName"] = stopName;
            return View(_context.StopTimesForStop(stopName, routeShortName, directionId, DateTime.Now));
        }
    }
}