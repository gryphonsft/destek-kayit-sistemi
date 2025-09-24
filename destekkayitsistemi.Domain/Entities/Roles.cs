using System.ComponentModel.DataAnnotations;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

    public class Roles
    {
         [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public ICollection<Users>? Users { get; set; }
    }

