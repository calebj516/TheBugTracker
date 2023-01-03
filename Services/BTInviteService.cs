using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTInviteService : IBTInviteService
    {
        private readonly ApplicationDbContext _context;

        public BTInviteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AcceptInviteAsync(Guid? token, string userId, int companyId)
        {
            // Retrieve the invite from the db
            Invite? invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);

            if (invite == null)
            {
                return false;
            }

            // Accept the invite
            try
            {
                invite.IsValid = false; // invite can no longer be accepted by anyone else
                invite.InviteeId = userId;
                await _context.SaveChangesAsync(); // save invite tracked by entity framework since line 20 to the db

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddNewInviteAsync(Invite invite)
        {
            try
            {
                await _context.Invites.AddAsync(invite);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AnyInviteAsync(Guid token, string email, int companyId)
        {
            try
            {
                bool result = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                    .AnyAsync(i => i.CompanyToken == token && i.InviteeEmail == email);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Invite> GetInviteAsync(int inviteId, int companyId)
        {
            try
            {
                Invite? invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                      .Include(i => i.Company)
                                                      .Include(i => i.Project)
                                                      .Include(i => i.Invitor)
                                                      .FirstOrDefaultAsync(i => i.Id == inviteId);

                return invite;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Invite> GetInviteAsync(Guid token, string email, int companyId)
        {
            try
            {
                Invite? invite = await _context.Invites.Where(i => i.CompanyId == companyId)
                                                       .Include(i => i.Company)
                                                       .Include(i => i.Project)
                                                       .Include(i => i.Invitor)
                                                       .FirstOrDefaultAsync(i => i.CompanyToken == token && i.InviteeEmail == email);

                return invite;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ValidateInviteCodeAsync(Guid? token)
        {
            if (token == null)
            {
                return false;
            }

            bool result = false;

            Invite? invite = await _context.Invites.FirstOrDefaultAsync(i => i.CompanyToken == token);

            if (invite != null)
            {
                // Determine invite date
                DateTime inviteDate = invite.InviteDate.DateTime;

                // Custom validation of invite based on the date it was issued
                // In this case, we are allowing an invite to be valid for 7 days
                bool validDate = (DateTime.Now - inviteDate).TotalDays <= 7;

                if (validDate)
                {
                    result = invite.IsValid;
                }
            }

            return result;
        }
    }
}
