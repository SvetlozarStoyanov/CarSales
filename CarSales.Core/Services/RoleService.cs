﻿using CarSales.Core.Contracts;
using CarSales.Core.Models.Roles;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> roleManager;
        public RoleService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<RoleListModel>> GetRequestableRolesAsync(IEnumerable<string> roleNames)
        {
            if (roleNames.Any(r => r == "Administrator" || r == "Salesman" || r == "Importer" || r == "Reviewer"))
            {
                return new List<RoleListModel>();
            }
            
            var roles = await roleManager.Roles
                .Where(r => !roleNames.Contains(r.Name))
                .Select(r => new RoleListModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                })
                .ToListAsync();
            return roles;
        }

        public async Task<IEnumerable<RoleListModel>> GetUserRolesAsync(IEnumerable<string> roleNames)
        {
            var roles = await roleManager.Roles
                .Where(r => roleNames.Contains(r.Name))
                .Select(r => new RoleListModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                })
                .ToListAsync();

            return roles;
        }
        public async Task<IEnumerable<RoleListModel>> GetRoleByIdsAsync(IEnumerable<string> roleIds)
        {
            var roles = await roleManager.Roles
                .Where(r => roleIds.Contains(r.Id))
                .Select(r => new RoleListModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                })
                .ToListAsync();
            return roles;
        }
    }
}
