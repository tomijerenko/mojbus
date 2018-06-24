using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Models;

namespace MojBus.Controllers
{
    public class HomeController : Controller
    {
        MojBusContext _context;

        public HomeController(MojBusContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
