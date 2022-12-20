using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheBugTracker.Models
{
    public class Invite
    {
        public int Id { get; set; }

        [DisplayName("Date Sent")]
        public DateTimeOffset InviteDate { get; set; }

        [DisplayName("Join Date")]
        public DateTimeOffset JoinDate { get; set; }

        [DisplayName("Code")]
        public Guid CompanyToken { get; set; }

        [DisplayName("Company")]
        public int CompanyId { get; set; } // Foreign Key

        [DisplayName("Project")]
        public int ProjectId { get; set; }

        // The two foreign keys below are strings because that is the type IdentityUser uses for its primary keys
        [DisplayName("Invitor")]
        public string? InvitorId { get; set; } // Foreign Key

        [DisplayName("Invitee")]
        public string? InviteeId { get; set; } // Foreign Key

        [DisplayName("Invitee Email")]
        public string? InviteeEmail { get; set; }

        [DisplayName("Invitee First Name")]
        public string? InviteeFirstName { get; set; }

        [DisplayName("Invitee Last Name")]
        public string? InviteeLastName { get; set; }

        public bool IsValid { get; set; }

        // Navigation Properties
        public virtual Company? Company { get; set; }
        public virtual BTUser? Invitor { get; set; }
        public virtual BTUser? Invitee { get; set; }
        public virtual Project? Project { get; set; }
    }
}
