using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.ViewModels;

    public class SettingsPageViewModel
    {
        public Users? Users { get; set; }
        public List<int>? selectedDeviceIds { get; set; }
        public List<Users>? UsersList { get; set; } // View'a tablo verisi için giden kullanıcılar verisi
        public List<Devices>? Devices { get; set; }
        public List<Tickets>? Tickets { get; set; }
        
    }
