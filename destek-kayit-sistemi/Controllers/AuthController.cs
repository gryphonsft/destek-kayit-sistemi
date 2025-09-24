using System.Security.Claims;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace destek_kayit_sistemi.Controllers;

public class AuthController : Controller
{

    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Login()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = _context.Users
            .Include(u => u.Roles)
            .FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user != null && user.Roles != null)
        {
            HttpContext.Session.SetInt32("Id", user.Id);
            HttpContext.Session.SetString("username", user.Username ?? string.Empty);
            HttpContext.Session.SetString("role", user.Roles.RoleName ?? string.Empty);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Roles.RoleName ?? string.Empty)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            switch (user.Roles.RoleName)
            {
                case "Admin":
                    return RedirectToAction("AdminView", "Home");
                case "Personel":
                    return RedirectToAction("PersonelView", "Home");
                case "Kullanıcı":
                    return RedirectToAction("UserView", "Home");
                default:
                    return RedirectToAction("Login", "Auth");
            }
        }

        ViewBag.Error = "Geçersiz giriş";
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Auth");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

}