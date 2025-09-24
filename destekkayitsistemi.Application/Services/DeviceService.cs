using destek_kayit_sistemi.destek_kayit_sistemi.Application.Interfaces;
using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.Services;

    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _context;

        public DeviceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Devices> getAll()
        {
            return _context.Devices.ToList();
        }
    }
