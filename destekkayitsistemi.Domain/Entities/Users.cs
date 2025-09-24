using System.ComponentModel.DataAnnotations.Schema;

namespace destek_kayit_sistemi.destek_kayit_sistemi.Domain.Entities;

   public class Users
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public int RoleId { get; set; }
    public Roles? Roles { get; set; }

    public ICollection<User_Devices>? User_Devices { get; set; }
    public ICollection<Tickets>? CreatedTickets { get; set; }
    public ICollection<Tickets>? AssignedTickets { get; set; }
}

