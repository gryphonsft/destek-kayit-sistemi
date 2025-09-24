using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.Interfaces;

    public interface IDeviceService
    {
        List<Devices> getAll();
    }
