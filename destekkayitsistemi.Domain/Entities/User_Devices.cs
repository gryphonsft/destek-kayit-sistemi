namespace destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

    public class User_Devices
    {
        public int UserId { get; set; }
        public Users? Users { get; set; }

        public int DeviceId { get; set; }
        public Devices? Devices { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }

