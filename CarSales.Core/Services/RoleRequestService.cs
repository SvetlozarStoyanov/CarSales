using AirsoftMatchMaker.Infrastructure.Data.Common.Repository;
using CarSales.Core.Contracts;
using CarSales.Core.Models.RoleRequests;
using CarSales.Core.Models.Users;
using CarSales.Infrastructure.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public async Task<IEnumerable<RoleRequestListModel>> GetAllRoleRequestsAsync()
        {
            var roleRequests = await repository.AllReadOnly<RoleRequest>()
                .Include(rrq => rrq.Role)
                .Include(rrq => rrq.User)
                .Select(rrq => new RoleRequestListModel()
                {
                    Id = rrq.Id,
                    RoleId = rrq.RoleId,
                    RoleName = rrq.Role.Name,
                    UserId = rrq.UserId,
                    UserName = rrq.User.UserName
                })
                .ToListAsync();
            return roleRequests;
        }

        public async Task<RoleRequestViewModel> GetRoleRequestByIdAsync(int id)
        {
            var roleRequest = await repository.AllReadOnly<RoleRequest>()
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

            var userRoles = await repository.AllReadOnly<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>()
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

        public async Task<IEnumerable<RoleRequestListModel>> GetRequestedRolesByUserIdAsync(string userId)
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
    }
}
