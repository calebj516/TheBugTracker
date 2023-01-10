using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(50)]
        [DisplayName("Title")]
        public string? Title { get; set; }

        [Required]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated")]
        public DateTimeOffset? Updated { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }

        [DisplayName("Archived By Project")]
        public bool ArchivedByProject { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; } // Foreign Key

        [DisplayName("Ticket Type")]
        public int TicketTypeId { get; set; } // Foreign Key

        [DisplayName("Ticket Priority")]
        public int TicketPriorityId { get; set; } // Foreign Key

        [DisplayName("Ticket Status")]
        public int TicketStatusId { get; set; } // Foreign Key

        [DisplayName("Ticket Owner")]
        public string? OwnerUserId { get; set; } // Foreign Key -- this is a string because it ties in to the built-in Identity, in which the primary keys are strings

        [DisplayName("Ticket Developer")]
        public string? DeveloperUserId { get; set; } // Foreign Key -- this is a string because it ties in to the built-in Identity, in which the primary keys are strings

        // Navigation Properties
        public virtual Project? Project { get; set; }
        public virtual TicketType? TicketType { get; set; }
        public virtual TicketPriority? TicketPriority { get; set; }
        public virtual TicketStatus? TicketStatus { get; set; }
        public virtual BTUser? OwnerUser { get; set; }
        public virtual BTUser? DeveloperUser { get; set; }

        // One-to-many relationships
        public virtual ICollection<TicketComment> Comments { get; set; } = new HashSet<TicketComment>();
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new HashSet<TicketAttachment>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public virtual ICollection<TicketHistory> History { get; set; } = new HashSet<TicketHistory>();
    }
}
