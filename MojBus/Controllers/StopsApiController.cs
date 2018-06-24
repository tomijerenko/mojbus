using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Helpers;

namespace MojBus.Controllers
{
    [Produces("application/json")]
    [Route("api/[action]")]
    public class StopsApiController : Controller
    {
        MojBusContext _context;

        public StopsApiController(MojBusContext context)
        {
            _context = context;
        }

        public IActionResult Stops()
        {
            return Json(StopsHelper.GetStops(_context));
        }

        [HttpGet]
        public IActionResult StopData(int stopId)
        {
            return Json(StopsHelper.GetStops(_context, stopId));
        }
    }
}