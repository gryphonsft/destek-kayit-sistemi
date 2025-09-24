namespace destek_kayit_sistemi.destek_kayit_sistemi.Application.DTOs;

    public class TicketCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public string? Category { get; set; }
        public int AssignedToUserId { get; set; }
    }
