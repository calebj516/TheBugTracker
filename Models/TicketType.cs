using System.ComponentModel;

namespace TheBugTracker.Models
{
    public class TicketType
    {
        public int Id { get; set; } // Primary Key

        [DisplayName("Type Name")]
        public string? Name { get; set; }
    }
}
