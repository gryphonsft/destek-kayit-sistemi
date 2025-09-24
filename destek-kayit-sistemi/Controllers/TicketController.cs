using destek_kayit_sistemi.destek_kayit_sistemi.Application.DTOs;
using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace destek_kayit_sistemi.Controllers;

    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketCreateDto dto)
        {

            var role = HttpContext.Session.GetString("role");
            if (!ModelState.IsValid)
                return View("Index", dto);

            var ticket = new Tickets
            {
                title = dto.Title,
                description = dto.Description,
                priority = dto.Priority,
                category = dto.Category,
                assigned_to_user_id = dto.AssignedToUserId,


                user_id = HttpContext.Session.GetInt32("Id") ?? 0,
                status = "Beklemede",
                created_at = DateTime.Now,
                closed_at = null
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Ticket basariyla olusturuldu!";

            switch (role)
            {
                case "Admin":
                    return RedirectToAction("AdminView", "Home");
                case "Personel":
                    return RedirectToAction("PersonelView", "Home");
                case "User":
                    return RedirectToAction("UserView", "Home");
                default:
                    return RedirectToAction("Login", "Auth");
            }
        }


        [Authorize(Roles = "Admin,Personel")]
        public IActionResult PersonelTicket(string category, string status)
        {
            var id = HttpContext.Session.GetInt32("Id");
            var username = HttpContext.Session.GetString("username");
            var rol = HttpContext.Session.GetString("role");

            ViewBag.Id = id;
            ViewBag.Role = rol;
            ViewBag.Username = username;

            List<Tickets> tickets;
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
            return View(tickets);
        }



        [HttpPost]
        public IActionResult UpdateTicket(Tickets ticket)
        {
            var existing = _context.Tickets.FirstOrDefault(t => t.id == ticket.id);

            if (existing != null)
            {

                existing.user_id = ticket.user_id;
                existing.status = ticket.status;

                if (ticket.status == "Kapalı" && existing.closed_at == null)
                {
                    existing.closed_at = DateTime.Now;
                }
                else if (ticket.status == "Açık" || ticket.status == "Beklemede")
                {
                    existing.closed_at = null;
                }
                _context.SaveChanges();
            }

            return RedirectToAction("PersonelTicket");
        }
    }
