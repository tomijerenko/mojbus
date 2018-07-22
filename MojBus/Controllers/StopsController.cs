﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Extensions;
using MojBus.Models;
using MojBus.Models.FavouriteStops;
using System;
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
                StopLocation = _context.GetStopLocation(stopName, directionId),
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
            StopTimetable timetable = new StopTimetable()
            {
                StopName = stopName,
                DirectionId = directionId,
                RouteShortName = routeShortName,
                StopLocation = _context.GetStopLocation(stopName, directionId),
                RequestedDate = date == DateTime.MinValue ? DateTime.Now : date,
            };
            timetable.StopTimetables = _context.StopTimesForStop(stopName, routeShortName, directionId, timetable.RequestedDate);

            return View(timetable);
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