using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTCompanyInfoService : IBTCompanyInfoService
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public BTCompanyInfoService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get All Members
        public async Task<List<BTUser>> GetAllMembersAsync(int companyId)
        {
            List<BTUser> result = new();
            result = await _context.Users.Where(u => u.CompanyId == companyId).ToListAsync();

            return result;
        }
        #endregion

        #region Get All Projects
        public async Task<List<Project>> GetAllProjectsAsync(int companyId)
        {
            List<Project> result = new();
            try
            {
                result = await _context.Projects.Where(p => p.CompanyId == companyId)
                                                    .Include(p => p.Members)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.Comments)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.Attachments)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.History)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.DeveloperUser)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.OwnerUser)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.Notifications)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.TicketStatus)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.TicketPriority)
                                                    .Include(p => p.Tickets)
                                                        .ThenInclude(t => t.TicketType)
                                                    .Include(p => p.ProjectPriority)
                                                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Get All Tickets
        public async Task<List<Ticket>> GetAllTicketsAsync(int companyId)
        {
            List<Ticket> result = new();
            List<Project> projects = new();

            projects = await GetAllProjectsAsync(companyId);
            result = projects.SelectMany(p => p.Tickets).ToList();

            return result;
        }
        #endregion

        #region Get Company Info By Id
        public async Task<Company> GetCompanyInfoByIdAsync(int? companyId)
        {
            Company? result = new();

            if (companyId != null)
            {
                result = await _context.Companies
                                        .Include(c => c.Members)
                                        .Include(c => c.Projects)
                                        .Include(c => c.Invites)
                                        .FirstOrDefaultAsync(c => c.Id == companyId);
            }

            return result;
        } 
        #endregion
    }
}
