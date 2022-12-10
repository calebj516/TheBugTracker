using System.ComponentModel;

namespace TheBugTracker.Models
{
    public class TicketPriority
    {
        public int Id { get; set; } // Primary Key

        [DisplayName("Priority Name")]
        public string? Name { get; set; }
    }
}
