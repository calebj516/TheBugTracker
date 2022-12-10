using System.ComponentModel;

namespace TheBugTracker.Models
{
    public class TicketStatus
    {
        public int Id { get; set; } // Primary Key

        [DisplayName("Status Name")]
        public string? Name { get; set; }
    }
}
