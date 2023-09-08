using CarSales.Core.Enums;
using CarSales.Core.Models.RoleRequests;
using CarSales.Infrastructure.Data.Entities;

namespace CarSales.Core.Contracts
{
    public interface IRoleRequestService
    {
        /// <summary>
        /// Returns <see cref="RoleRequestsQueryModel"/> and allows filtering and sorting of
        /// Role Requests
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="currentPage"></param>
        /// <param name="roleRequestsPerPage"></param>
        /// <param name="selectedUserName"></param>
        /// <param name="selectedRoleNames"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<RoleRequestsQueryModel> GetRoleRequestsAsync(string searchTerm = null,
            int currentPage = 1,
            int roleRequestsPerPage = 12,
            string selectedUserName = null,
            string selectedRoleNames = null,
            RoleRequestSorting sorting = RoleRequestSorting.Newest);

        /// <summary>
        /// Returns roles requested by user with given Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<RoleRequestListModel>> GetRoleRequestsByUserIdAsync(string userId);

        /// <summary>
        /// Returns role request with given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="RoleRequestViewModel"/></returns>
        Task<RoleRequestViewModel> GetRoleRequestByIdAsync(int id);

        /// <summary>
        /// Creates a role request with <see cref="Role"/> with <paramref name="roleId"/>
        /// from <see cref="User"/> with <paramref name="userId"/>
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task CreateRoleRequestAsync(string roleId, string userId);

        /// <summary>
        /// !DEPRECATED!
        /// Deletes a role request with given <paramref name="roleId"/>
        /// and <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteRoleRequestAsync(string roleId, string userId);

        /// <summary>
        /// Deletes RoleRequest with given <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRoleRequestAsync(int id);


    }
}
