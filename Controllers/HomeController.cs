using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApiAuthetication.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _config { get; }

        public HomeController(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login([FromQuery] string uname, [FromQuery] string password)
        {
            if (uname == "admin" && password == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim("name", uname),
                    new Claim(ClaimTypes.Role, "admin"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                };

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

                return RedirectToAction("Customer");
            }
            return RedirectToAction("Login");
        }

        [Route("login/customers")]
        [HttpGet]
        [Authorize(Policy = "admin")]
        public IActionResult Customer()
        {
            var test = HttpContext.User.Claims.Where((Claim claim) => claim.Type == "name").FirstOrDefault();
            var returnUrl = Request.Query["returnurl"].ToString();
            return Json(new { foo = "bar", baz = "Blech" });
        }
    }
}