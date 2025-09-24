using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.Interfaces;

    public interface IUserDeviceService
    {
        IQueryable<User_Devices> getAll();
        void Add(User_Devices userDevice);
    }
