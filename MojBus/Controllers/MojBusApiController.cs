using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Extensions;
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
            return Json(_context.GetStops());
        }

        public IActionResult Routes()
        {
            return Json(_context.GetRoutes());
        }

        [HttpGet]
        public IActionResult StopData(string stopName, DateTime date)
        {
            return Json(_context.StopTimesForStop(stopName, date));
        }

        [HttpGet]
        public IActionResult StopDataForTripDirection(string stopName, int directionId, DateTime date)
        {
            return Json(_context.StopTimesForStop(stopName, directionId, date));
        }

        [HttpGet]
        public IActionResult StopDataForRoute(string stopName, string routeShortName, DateTime date)
        {
            return Json(_context.StopTimesForStop(stopName, routeShortName, date));
        }

        [HttpGet]
        public IActionResult StopDataForRouteTrip(string stopName, string routeShortName, string tripHeadSign, DateTime date)
        {
            return Json(_context.StopTimesForStop( stopName, routeShortName, tripHeadSign, date));
        }
    }
}