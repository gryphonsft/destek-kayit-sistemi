using System.ComponentModel.DataAnnotations;
namespace destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

    public class Tickets
    {
        public int id { get; set; }

        public int user_id { get; set; }
        public Users? Users { get; set; }

        public string? title { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
        public string? priority { get; set; }
        public string? category { get; set; }

        public int assigned_to_user_id { get; set; }
        public Users? AssignedToUser { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? closed_at { get; set; }

    }

