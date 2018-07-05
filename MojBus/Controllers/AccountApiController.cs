using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MojBus.Models;
using MojBus.Models.AccountApiModels;

namespace MojBus.Controllers
{
    [Produces("application/json")]
    [Route("api/[action]")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountApiController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterApiModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string userId = await _userManager.GetUserIdAsync(user);

                    return Json(new
                    {
                        result.Succeeded,
                        userId,
                    });
                }
                
                return Json(result);
            }
            
            return Json(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginApiModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    string userId = await _userManager.GetUserIdAsync(user);

                    return Json(new
                    {
                        result.Succeeded,
                        userId
                    });
                }

                return Json(result);
            }

            return Json(ModelState);
        }
    }
}
