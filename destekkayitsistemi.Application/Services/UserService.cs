using destek_kayit_sistemi.destek_kayit_sistemi.Application.Interfaces;
using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;
using destek_kayit_sistemi.destek_kayit_sistemi.Infrastructure.Data;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.Services;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Users> getAll()
        {
            return _context.Users.ToList();
        }
    }
