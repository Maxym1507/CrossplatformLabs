using Auth0.AspNetCore.Authentication;
using Lab5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task SignIn()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder().WithRedirectUri(Url.Action("Index", "Home")).Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [HttpPost]
        public async Task SignOut()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder().WithRedirectUri("/").Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var test = HttpContext.User.Claims.Select(cl => cl.Type).ToList();
            return View(new UserProfileModel()
            {
                UserName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value.ToString()??"Name not found",
                Email = HttpContext.User.Claims.FirstOrDefault(x => x.Type.EndsWith("emailaddress"))?.Value.ToString()??"Email not found",
                EmailVerified = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email_verified")?.Value.ToString() ?? "Email verification not found"
            });
        }
    }
}
