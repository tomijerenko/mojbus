using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MojBus.Data;
using MojBus.Extensions;
using MojBus.Models;
using System.Threading.Tasks;

namespace MojBus.ViewComponents
{
    [Authorize]
    public class FavouriteStopsViewComponent : ViewComponent
    {
        MojBusContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavouriteStopsViewComponent(MojBusContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            return View(_context.GetFavouriteStopsLoggedIn(user.Id));
        }
    }
}
