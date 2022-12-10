using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheBugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; } // Primary Key

        [DisplayName("Ticket")]
        public int TicketId { get; set; } // Foreign Key

        [DisplayName("Updated Item")]
        public string? Property { get; set; }

        [DisplayName("Previous")]
        public string? OldValue { get; set; }

        [DisplayName("Current")]
        public string? NewValue { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date Modified")]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Description of Change")]
        public string? Descripton { get; set; }

        [DisplayName("Team Member")]
        public string? UserId { get; set; } // Foreign Key

        // Navigation Properties
        public virtual Ticket? Ticket { get; set; }
        public virtual BTUser? User { get; set; }
    }
}
