using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheBugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; } // Primary Key

        [DisplayName("Member Comment")]
        public string? Comment { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // Foreign Key

        [DisplayName("Team Member")]
        public string? UserId { get; set; } // Foreign Key

        // Navigation Properties
        public virtual Ticket? Ticket { get; set; }
        public virtual BTUser? User { get; set; }
    }
}
