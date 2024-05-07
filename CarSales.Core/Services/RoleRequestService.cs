﻿using CarSales.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Enums;
using CarSales.Core.Models.RoleRequests;
using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Core.Services
{
    public class RoleRequestService : IRoleRequestService
    {
        private readonly IRepository repository;
        public RoleRequestService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<RoleRequestsQueryModel> GetRoleRequestsAsync(string searchTerm = null,
            int currentPage = 1,
            int roleRequestsPerPage = 12,
            string selectedUserName = null,
            string selectedRoleNames = null,
            RoleRequestSorting sorting = RoleRequestSorting.Newest)
        {
            var roleRequests = await repository.AllReadOnly<RoleRequest>()
                .Include(rrq => rrq.Role)
                .Include(rrq => rrq.User)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(selectedRoleNames))
            {
                var selectedRoles = selectedRoleNames.Split(';', StringSplitOptions.RemoveEmptyEntries)
                      .ToList();
                roleRequests = roleRequests
                     .Where(rrq => selectedRoles.Contains(rrq.Role.Name))
                     .ToList();
            }
            var userNames = roleRequests
                .Select(rrq => rrq.User.UserName)
                .ToHashSet();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                roleRequests = roleRequests
                    .Where(rrq => rrq.User.UserName.ToLower().Contains(searchTerm.ToLower())
                    || rrq.Role.Name.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(selectedUserName))
            {
                roleRequests = roleRequests
                    .Where(rrq => rrq.User.UserName == selectedUserName)
                    .ToList();
            }

            switch (sorting)
            {
                case RoleRequestSorting.Newest:
                    roleRequests = roleRequests
                          .OrderByDescending(rrq => rrq.Id)
                          .ToList();
                    break;
                case RoleRequestSorting.Oldest:
                    roleRequests = roleRequests
                          .OrderBy(rrq => rrq.Id)
                          .ToList();
                    break;
            }
            var roleRequestCount = roleRequests.Count;
            var sortedRoleRequests = roleRequests
                    .Skip(roleRequestsPerPage * (currentPage - 1))
                    .Take(roleRequestsPerPage)
                    .Select(rrq => new RoleRequestListModel()
                    {
                        Id = rrq.Id,
                        RoleId = rrq.RoleId,
                        RoleName = rrq.Role.Name,
                        UserId = rrq.UserId,
                        UserName = rrq.User.UserName
                    })
                   .ToList();
            var queryModel = await CreateRoleRequestsQueryModel(searchTerm, currentPage, roleRequestsPerPage, selectedUserName, selectedRoleNames, sortedRoleRequests, userNames, roleRequestCount);
            return queryModel;
        }

        public async Task<IEnumerable<RoleRequestListModel>> GetRoleRequestsByUserIdAsync(string userId)
        {
            var requestedRoles = await repository.AllReadOnly<RoleRequest>()
                .Where(rrq => rrq.UserId == userId)
                .Select(rrq => new RoleRequestListModel()
                {
                    Id = rrq.Id,
                    RoleId = rrq.RoleId,
                    RoleName = rrq.Role.Name,
                    UserId = rrq.UserId,
                    UserName = rrq.User.UserName
                })
                .ToListAsync();
            return requestedRoles;
        }

        public async Task<RoleRequestViewModel> GetRoleRequestByIdAsync(int id)
        {
            var roleRequest = await repository.AllReadOnly<RoleRequest>()
               .Where(rrq => rrq.Id == id)
               .Include(rrq => rrq.Role)
               .Include(rrq => rrq.User)
               .Select(rrq => new RoleRequestViewModel()
               {
                   Id = rrq.Id,
                   RoleId = rrq.RoleId,
                   RoleName = rrq.Role.Name,
                   UserModel = new UserWithRolesModel()
                   {
                       Id = rrq.UserId,
                       FirstName = rrq.User.FirstName,
                       LastName = rrq.User.LastName,
                       UserName = rrq.User.UserName,
                       Gender = rrq.User.Gender,
                       ImageUrl = rrq.User.ImageUrl,
                       Email = rrq.User.Email
                   }
               })
               .FirstOrDefaultAsync();
            if (roleRequest == null)
            {
                return null;
            }
            var userRoles = await repository.AllReadOnly<IdentityUserRole<string>>()
                .Where(ur => ur.UserId == roleRequest.UserModel.Id)
                .Select(ur => ur.RoleId)
                .ToListAsync();
            var roles = await repository.AllReadOnly<Role>()
                .Where(r => userRoles.Contains(r.Id))
                .Select(r => r.Name)
                .ToListAsync();
            roleRequest.UserModel.Roles = roles.ToHashSet();
            return roleRequest;
        }

        public async Task CreateRoleRequestAsync(string roleId, string userId)
        {
            if (repository.All<RoleRequest>().Any(rrq => rrq.RoleId == roleId && rrq.UserId == userId))
            {
                return;
            }

            var roleRequest = new RoleRequest()
            {
                RoleId = roleId,
                UserId = userId
            };
            await repository.AddAsync<RoleRequest>(roleRequest);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteRoleRequestAsync(string roleId, string userId)
        {
            if (!repository.All<RoleRequest>().Any(rrq => rrq.RoleId == roleId && rrq.UserId == userId))
            {
                return;
            }
            var roleRequest = await repository.All<RoleRequest>().FirstOrDefaultAsync(rrq => rrq.RoleId == roleId && rrq.UserId == userId);
            if (roleRequest == null)
            {
                return;
            }
            repository.Delete<RoleRequest>(roleRequest);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteRoleRequestAsync(int id)
        {
            var roleRequest = await repository.GetByIdAsync<RoleRequest>(id);
            if (roleRequest == null)
            {
                return;
            }
            repository.Delete<RoleRequest>(roleRequest);
            await repository.SaveChangesAsync();
        }






        private async Task<RoleRequestsQueryModel> CreateRoleRequestsQueryModel(string searchTerm, int currentPage, int roleRequestsPerPage, string selectedUserName, string selectedRoleNames, List<RoleRequestListModel> roleRequests, HashSet<string?> userNames, int roleRequestCount)
        {
            var model = new RoleRequestsQueryModel()
            {
                SearchTerm = searchTerm,
                CurrentPage = currentPage,
                RoleRequestsPerPage = roleRequestsPerPage,
                MaxPage = (int)Math.Ceiling(roleRequestCount / (double)roleRequestsPerPage),
                RoleRequestCount = roleRequestCount,
                SelectedUserName = selectedUserName,
                SelectedRoleNames = selectedRoleNames,
                RoleRequests = roleRequests,
                UserNames = userNames
            };
            var roleNames = await repository.AllReadOnly<Role>()
                .Where(r => r.Name != "Owner" && r.Name != "Administrator")
                .Select(r => r.Name)
                .ToListAsync();
            model.RoleNames = roleNames.ToHashSet();

            if (model.MaxPage > 1)
            {
                var previousPages = new HashSet<int>();
                var nextPages = new HashSet<int>();
                //var pagesToMaxPage = model.MaxPage - model.CurrentPage;
                var numberOfPages = 0;
                var index = 1;
                while (numberOfPages < 4 && numberOfPages < model.MaxPage - 1)
                {
                    var previousPage = model.CurrentPage - index;
                    var nextPage = model.CurrentPage + index;
                    if (previousPage >= 1)
                    {
                        previousPages.Add(previousPage);
                        numberOfPages++;
                    }
                    if (nextPage <= model.MaxPage)
                    {
                        nextPages.Add(nextPage);
                        numberOfPages++;
                    }
                    index++;
                }
                model.PreviousPages = previousPages.Reverse();
                model.NextPages = nextPages;
            }
            return model;
        }

    }
}
