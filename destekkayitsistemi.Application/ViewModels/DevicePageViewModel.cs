using destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.ViewModels;

    public class DevicePageViewModel
    {
        // cihazları listelemek için model
        public List<Devices>? Devices { get; set; }

        // cihaz devretmek için model
        public List<Users>? Users { get; set; }
        public int SelectedDeviceid { get; set; }
        public int SelectedUserid { get; set; }

        public List<User_Devices>? UserDevices { get; set; }

    }
