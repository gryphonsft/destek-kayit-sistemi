using System.Diagnostics;
using destek_kayit_sistemi.destek_kayit_sistemi.Application.ViewModels;
using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace destek_kayit_sistemi.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("anasayfa-admin")]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminView()
    {
        var assignedUsers = _context.Users
          .Where(u => u.RoleId == 2)
          .Select(u => new SelectListItem
          {
              Value = u.Id.ToString(),
              Text = u.Username
          })
           .ToList();

        var tickets = _context.Tickets
        .Include(t => t.AssignedToUser)
        .OrderByDescending(t => t.created_at)
        .ToList();

        //Hali hazırdaki session'ı yakalayıp viewbag olarak view sayfasına gönderme.
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");

        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;
        ViewBag.AssignedUsers = assignedUsers;

        return View("AdminView", tickets);
    }


    [Route("anasayfa-personel")]
    [HttpGet]
    [Authorize(Roles = "Personel,Admin")]
    public IActionResult PersonelView()
    {
        var assignedUsers = _context.Users
                    .Where(u => u.RoleId == 2)
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Username
                    })
                    .ToList();

        var tickets = _context.Tickets
        .Include(t => t.AssignedToUser)
        .OrderByDescending(t => t.created_at)
        .ToList();

        //Hali hazırdaki session'ı yakalayıp viewbag olarak view sayfasına gönderme.
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");

        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;
        ViewBag.AssignedUsers = assignedUsers;


        return View("PersonelView", tickets);
    }


    [Authorize(Roles = "Kullanıcı")]
    public IActionResult UserView()
    {

        var assignedUsers = _context.Users
        .Where(u => u.RoleId == 2)
        .Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.Username
        })
        .ToList();

        ViewBag.AssignedUsers = assignedUsers;

        //Hali hazırdaki session'ı yakalayıp viewbag olarak view sayfasına gönderme.
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");


        var tickets = _context.Tickets
        .Include(t => t.AssignedToUser)
        .Where(t => t.user_id == id)
        .OrderByDescending(t => t.created_at)
        .ToList();


        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;

        return View("UserView", tickets);
    }
    [Route("raporlama")]
    [HttpGet]
    public IActionResult ReportView()
    {
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");

        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;


        return View();
    }

    [Route("ayarlar")]
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult SettingsView()
    {
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");

        var users = _context.Users.
            Include(u => u.Roles)
           .ToList();

        var model = new SettingsPageViewModel
        {
            Users = new Users(),

            UsersList = _context.Users.
            Include(u => u.Roles)
           .Include(u => u.CreatedTickets)
           .Include(u => u.User_Devices)
           .ToList(),
            Devices = _context.Devices.ToList()
        };


        ViewBag.Roles = _context.Roles.ToList();
        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;


        return View(model);
    }
    [Route("testalanı")]
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult Testalanı(string category, string status)
    {
        // Personel yetkisine sahip kullanıcıların, view'a gönderilmesi.
        var assignedUsers = _context.Users
          .Where(u => u.RoleId == 2)
          .Select(u => new SelectListItem
          {
              Value = u.Id.ToString(),
              Text = u.Username
          })
           .ToList();

        ViewBag.Tickets = _context.Tickets.Count();
        var tickets = _context.Tickets
        .Include(t => t.AssignedToUser)
        .OrderByDescending(t => t.created_at)
        .ToList();

        var role = HttpContext.Session.GetString("role");
        var username = HttpContext.Session.GetString("username");

        if (!string.IsNullOrEmpty(category))
        {

            tickets = _context.Tickets
            .Include(t => t.Users)
            .Include(t => t.AssignedToUser)
            .OrderByDescending(t => t.created_at)
            .Where(t => t.category == category)
            .ToList();

        }
        else if (!string.IsNullOrEmpty(status))
        {

            tickets = _context.Tickets
            .Include(t => t.Users)
            .Include(t => t.AssignedToUser)
            .OrderByDescending(t => t.created_at)
            .Where(t => t.status == status)
            .ToList();

        }
        else
        {

            tickets = _context.Tickets
            .Include(t => t.Users)
            .Include(t => t.AssignedToUser)
            .OrderByDescending(t => t.created_at)
            .ToList();

        }

        ViewBag.AssignedUsers = assignedUsers; // Personel atama için viewbag
        ViewBag.Role = role;
        ViewBag.Username = username;
        return View(tickets);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> userCreate(SettingsPageViewModel model)
    {
        var role = HttpContext.Session.GetString("role");
        if (model.Users == null)
        {
            ModelState.AddModelError("", "Kullanıcı bilgileri eksik.");
            return View(model);
        }
        var users = new Users
        {
            Username = model.Users.Username,
            Password = model.Users.Password,
            RoleId = model.Users.RoleId
        };
        _context.Users.Add(users);
        await _context.SaveChangesAsync();

        switch (role)
        {
            case "Admin":
                return RedirectToAction("SettingsView", "Home");
            default:
                return RedirectToAction("Login", "Auth");
        }
    }

    //roleCreate Action eklenecek

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult UserTickets(int Id)
    {
        var id = HttpContext.Session.GetInt32("Id");
        var rol = HttpContext.Session.GetString("role");
        var username = HttpContext.Session.GetString("username");

        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;

        var user = _context.Users?
            .Include(u => u.CreatedTickets!)
            .ThenInclude(t => t.AssignedToUser)
            .FirstOrDefault(u => u.Id == Id);

        if (user == null)
            return NotFound();

        return View(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult deleteTicket(int id)
    {
        var ticket = _context.Tickets.Find(id);

        if (ticket == null)
        {
            return NotFound();
        }
        _context.Tickets.Remove(ticket);
        _context.SaveChanges();

        return RedirectToAction("SettingsView");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult updateUser(SettingsPageViewModel multimodel)
    {
        if (multimodel.Users == null)
        {
            ModelState.AddModelError("", "Kullanıcı bilgileri eksik.");
            return View(multimodel);
        }

        bool isUserexist = _context.Users.Any(u => u.Username == multimodel.Users.Username && u.Id != multimodel.Users.Id);

        var existing = _context.Users
            .Include(u => u.User_Devices)
            .FirstOrDefault(u => u.Id == multimodel.Users.Id);

        if (existing == null)
        {
            return NoContent();
        }

        // username, password, roleid, deviceid
        existing.Username = multimodel.Users.Username;
        existing.Password = multimodel.Users.Password;

        // RoleId sadece geçerli bir değer gönderildiyse güncellensin
        if (multimodel.Users.RoleId > 0 && _context.Roles.Any(r => r.RoleId == multimodel.Users.RoleId))
        {
            existing.RoleId = multimodel.Users.RoleId;
        }

        if (multimodel.selectedDeviceIds != null)
        {
            var existingLinks = _context.User_Devices.Where(ud => ud.UserId == existing.Id).ToList();
            if (existingLinks.Count > 0)
                _context.User_Devices.RemoveRange(existingLinks);

            var distinctIds = multimodel.selectedDeviceIds.Distinct();
            var newUserDevices = distinctIds.Select(d => new User_Devices
            {
                UserId = existing.Id,
                DeviceId = d,
                AssignedAt = DateTime.Now
            }).ToList();

            if (newUserDevices.Count > 0)
                _context.User_Devices.AddRange(newUserDevices);
        }

        _context.SaveChanges();

        return RedirectToAction("SettingsView");
    }


    [HttpGet]
    public IActionResult About()
    {
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");

        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

