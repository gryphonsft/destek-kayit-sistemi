using System.ComponentModel.DataAnnotations;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

    public class Devices
    {
        public int DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceType { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

       
        public ICollection<User_Devices>? User_Devices { get; set; }
    }

