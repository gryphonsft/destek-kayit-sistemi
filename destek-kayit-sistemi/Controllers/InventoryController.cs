using System.Diagnostics;
using destek_kayit_sistemi.destek_kayit_sistemi.Application.Interfaces;
using destek_kayit_sistemi.destek_kayit_sistemi.Application.ViewModels;
using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace destek_kayit_sistemi.Controllers;


public class InventoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IDeviceService _deviceService;
    private readonly IUserDeviceService _userDeviceService;
    private readonly IUserService _userService;


    public InventoryController(ApplicationDbContext context, IDeviceService deviceService, IUserDeviceService userDeviceService, IUserService userService)
    {
        _context = context;
        _deviceService = deviceService;
        _userDeviceService = userDeviceService;
        _userService = userService;


    }
    [Route("envanter")]
    public IActionResult Index()
    {
        var id = HttpContext.Session.GetInt32("Id");
        var username = HttpContext.Session.GetString("username");
        var rol = HttpContext.Session.GetString("role");

        ViewBag.Id = id;
        ViewBag.Role = rol;
        ViewBag.Username = username;

        var model = new DevicePageViewModel
        {
            Devices = _deviceService.getAll(),
            UserDevices = _userDeviceService.getAll().ToList(),
            Users = _userService.getAll()
        };

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> deviceCreate(Devices devices)
    {
        var role = HttpContext.Session.GetString("role");

        var devi = new Devices
        {
            DeviceName = devices.DeviceName,
            DeviceType = devices.DeviceType,
            Brand = devices.Brand,
            Model = devices.Model,

            IsActive = devices.IsActive,
            CreatedAt = DateTime.Now,
            UpdatedAt = null
        };
        _context.Devices.Add(devi);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Cihaz başarıyla eklendi!";

        switch (role)
        {
            case "Admin":
                return RedirectToAction("Index", "Inventory");
            default:
                return RedirectToAction("Login", "Auth");
        }
    }
    [HttpPost]
    public IActionResult deviceTransfer(DevicePageViewModel model)
    {
        var userdevice = new User_Devices
        {
            UserId = model.SelectedUserid,
            DeviceId = model.SelectedDeviceid

        };

        _userDeviceService.Add(userdevice);

        return RedirectToAction("Index");
    }

    public IActionResult allDeviceTransfer()
    {
        var transferredDevices = _userDeviceService.getAll()
    .Include(ud => ud.Users)
    .Include(ud => ud.Devices)
    .ToList();

        var model = new DevicePageViewModel
        {
            UserDevices = transferredDevices
        };

        return View(model);
    }

}
