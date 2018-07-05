using Microsoft.AspNetCore.Mvc;
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
            ViewData["StopName"] = stopName;
            ViewData["DirectionId"] = directionId;
            return View(_context.StopTimesForStop(stopName, directionId, DateTime.Now));
        }

        public IActionResult StopDataForRoute(string stopName, string routeShortName, string tripHeadSign)
        {
            ViewData["StopName"] = stopName;
            return View(_context.StopTimesForStop(stopName, routeShortName, tripHeadSign, DateTime.Now));
        }
    }
}