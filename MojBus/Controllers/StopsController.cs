using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Extensions;
using MojBus.Models;
using MojBus.Models.FavouriteStops;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MojBus.Controllers
{
    public class StopsController : Controller
    {
        MojBusContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public StopsController(MojBusContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string stopName, int directionId, DateTime date)
        {
            StopTimetable timetable = new StopTimetable()
            {
                StopName = stopName,
                DirectionId = directionId,
                StopLocation = _context.StopLocation(stopName, directionId),
                RequestedDate = date == DateTime.MinValue ? DateTime.Now : date,
            };

            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                timetable.StopTimetables = _context.StopTimesForStopLoggedIn(stopName, directionId, user.Id, timetable.RequestedDate);

                return View(timetable);
            }

            timetable.StopTimetables = _context.StopTimesForStop(stopName, directionId, timetable.RequestedDate);

            return View(timetable);
        }

        public IActionResult Timetable(string stopName, string routeShortName, int directionId, DateTime date)
        {
            var gtfsline = _context.Gtfslines.FirstOrDefault(x => x.TripShortName == routeShortName);
            StopTimetable timetable = new StopTimetable()
            {
                StopName = stopName,
                DirectionId = directionId,
                TripShortName = routeShortName,
                RouteShortName = gtfsline.Code,
                HTMLColor = gtfsline.Htmlcolor,
                RouteStopLocations = _context.LocationsForRouteStops(routeShortName, directionId, date),
                StopLocation = _context.StopLocation(stopName, directionId),
                RequestedDate = date == DateTime.MinValue ? DateTime.Now : date,
            };
            timetable.StopTimetables = _context.StopTimesForStop(stopName, routeShortName, directionId, timetable.RequestedDate);

            return View(timetable);
        }

        public IActionResult TripPlanner(string stopFrom, string stopTo, DateTime date)
        {
            TripPlannerModel model = _context.DepartureTimetableBetweenTwoStops(stopFrom, stopTo, date);
            model.SearchedDate = date;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToFavourite(FavouriteStopRouteModel favouriteStop)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            favouriteStop.UserId = user.Id;

            return Json(_context.AddStopRouteToFavourites(favouriteStop));
        }
    }
}