using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Extensions;
using MojBus.Models.FavouriteStops;
using System;
using System.Linq;

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
            return Json(_context.StopTimesForStop(stopName, routeShortName, tripHeadSign, date));
        }

        //new api methods
        [HttpGet]
        public IActionResult RoutesWithDepartureTimesForStop(string stopName, int directionId, DateTime date)
        {
            return Json(_context.StopTimesForStop(stopName, directionId, date));
        }

        [HttpGet]
        public IActionResult RouteDepartureTimesForStop(string stopName, string routeShortName, int directionId, DateTime date)
        {
            return Json(_context.StopTimesForStop(stopName, routeShortName, directionId, date));
        }

        [HttpGet]
        public IActionResult StopDataLoggedIn(string stopName, int directionId, string userId, DateTime date)
        {
            return Json(_context.StopTimesForStopLoggedIn(stopName, directionId, userId, date));
        }

        [HttpGet]
        public IActionResult GetFavouriteStopRouteData(string userId)
        {
            return Json(_context.GetFavouriteStopsLoggedIn(userId));
        }

        [HttpGet]
        public IActionResult Lines()
        {
            return Json(_context.Gtfslines.ToList());
        }

        [HttpPost]
        public IActionResult AddStopRouteToFavourites([FromBody]FavouriteStopRouteModel favouriteStop)
        {
            return Json(new
            {
                succeeded = _context.AddStopRouteToFavourites(favouriteStop)
            });
        }

        [HttpGet]
        public IActionResult RouteStopsData(string routeShortName, DateTime date)
        {
            return Json(_context.RouteStopTimes(routeShortName, date));
        }

        [HttpGet]
        public IActionResult RouteStopsDataForDirection(string routeShortName, int directionId, DateTime date)
        {
            return Json(_context.RouteStopTimes(routeShortName, directionId, date));
        }

        [HttpGet]
        public IActionResult StopLocation(string stopName, int directionId)
        {
            return Json(_context.StopLocation(stopName, directionId));
        }

        [HttpGet]
        public IActionResult LocationsForRouteStops(string routeShortName, int directionId, DateTime date)
        {
            return Json(_context.LocationsForRouteStops(routeShortName, directionId, date));
        }

        [HttpGet]
        public IActionResult DepartureTimetableBetweenTwoStops(string stopNameFrom, string stopNameTo, DateTime date)
        {
            return Json(_context.DepartureTimetableBetweenTwoStops(stopNameFrom, stopNameTo, date));
        }
    }
}