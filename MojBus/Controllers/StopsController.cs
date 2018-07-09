using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index(string stopName, int directionId)
        {
            ViewData["StopName"] = stopName;
            ViewData["DirectionId"] = directionId;


            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

                return View(_context.StopTimesForStopLoggedIn(stopName, directionId, user.Id, DateTime.Now));
            }                

            return View(_context.StopTimesForStop(stopName, directionId, DateTime.Now));
        }

        public IActionResult StopDataForRoute(string stopName, string routeShortName, int directionId)
        {
            ViewData["StopName"] = stopName;            

            return View(_context.StopTimesForStop(stopName, routeShortName, directionId, DateTime.Now));
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