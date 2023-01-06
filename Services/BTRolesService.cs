﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTRolesService : IBTRolesService
    {
        #region Properties
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;
        #endregion

        #region Constructor
        public BTRolesService(ApplicationDbContext context,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<BTUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        } 
        #endregion

        #region Get Roles
        public async Task<List<IdentityRole>> GetRolesAsync()
        {
            try
            {
                List<IdentityRole> result = new();
                result = await _context.Roles.ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Add User To Role
        public async Task<bool> AddUserToRoleAsync(BTUser user, string roleName)
        {
            bool result = (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
            return result;
        }
        #endregion

        #region Get Role Name By Id
        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {
            IdentityRole? role = _context.Roles.Find(roleId);
            string result = await _roleManager.GetRoleNameAsync(role);
            return result;
        }
        #endregion

        #region Get User Roles
        public async Task<IEnumerable<string>> GetUserRolesAsync(BTUser user)
        {
            IEnumerable<string> result = await _userManager.GetRolesAsync(user);
            return result;
        }
        #endregion

        #region Get Users In Role
        public async Task<List<BTUser>> GetUsersInRoleAsync(string roleName, int companyId)
        {
            List<BTUser> users = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
            List<BTUser> result = users.Where(u => u.CompanyId == companyId).ToList();
            return result;
        }
        #endregion

        #region Get Users Not In Role
        public async Task<List<BTUser>> GetUsersNotInRoleAsync(string roleName, int companyId)
        {
            List<string> userIds = (await _userManager.GetUsersInRoleAsync(roleName)).Select(u => u.Id).ToList();
            List<BTUser> roleUsers = _context.Users.Where(u => !userIds.Contains(u.Id)).ToList();

            List<BTUser> result = roleUsers.Where(u => u.CompanyId == companyId).ToList();

            return result;
        }
        #endregion

        #region Is User In Role
        public async Task<bool> IsUserInRoleAsync(BTUser user, string roleName)
        {
            bool result = await _userManager.IsInRoleAsync(user, roleName);
            return result;
        }
        #endregion

        #region Remove User From Role
        public async Task<bool> RemoveUserFromRoleAsync(BTUser user, string roleName)
        {
            bool result = (await _userManager.RemoveFromRoleAsync(user, roleName)).Succeeded;
            return result;
        }
        #endregion

        #region Remove User From Roles
        public async Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            bool result = (await _userManager.RemoveFromRolesAsync(user, roles)).Succeeded;
            return result;
        } 
        #endregion
    }
}
