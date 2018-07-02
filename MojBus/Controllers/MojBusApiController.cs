using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Helpers;
using System;

namespace MojBus.Controllers
{
    [Produces("application/json")]
    [Route("api/[action]")]
    public class MojBusApiController : Controller
    {
        MojBusContext _context;

        public MojBusApiController(MojBusContext context)
        {
            _context = context;
        }

        public IActionResult Stops()
        {
            return Json(MojBusHelper.GetStops(_context));
        }

        public IActionResult Routes()
        {
            return Json(MojBusHelper.GetRoutes(_context));
        }

        [HttpGet]
        public IActionResult StopData(string stopName, DateTime date)
        {
            return Json(MojBusHelper.StopTimesForStop(_context, stopName, date));
        }

        [HttpGet]
        public IActionResult StopDataForRoute(string stopName, string routeShortName, DateTime date)
        {
            return Json(MojBusHelper.StopTimesForStop(_context, stopName, routeShortName, date));
        }

        [HttpGet]
        public IActionResult StopDataForRouteTrip(string stopName, string routeShortName, string tripHeadSign, DateTime date)
        {
            return Json(MojBusHelper.StopTimesForStop(_context, stopName, routeShortName, tripHeadSign, date));
        }
    }
}