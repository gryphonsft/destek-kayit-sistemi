using destek_kayit_sistemi.destek_kayit_sistemi.Application.Interfaces;
using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.Services;
    public class UserDeviceService : IUserDeviceService
    {
        private readonly ApplicationDbContext _context;

        public UserDeviceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<User_Devices> getAll()
        {
            return _context.User_Devices.AsQueryable();
        }
        public void Add(User_Devices userDevice)
        {
            _context.User_Devices.Add(userDevice);
            _context.SaveChanges();
        }
    }
