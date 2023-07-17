using CarSales.Core.Enums;

namespace CarSales.Core.Models.RoleRequests
{
    public class RoleRequestsQueryModel
    {
        public RoleRequestsQueryModel()
        {
            UserNames = new HashSet<string>();
            RoleNames = new HashSet<string>();
            SortingOptions = Enum.GetValues<RoleRequestSorting>().ToHashSet();
            RoleRequests = new List<RoleRequestListModel>();
        }

        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int RoleRequestsPerPage { get; set; } = 12;
        public int FirstPage { get; set; } = 1;
        public int MaxPage { get; set; }
        public string? SelectedUserName { get; set; }
        public string? SelectedRoleNames { get; set; }
        public RoleRequestSorting Sorting { get; set; }
        public ICollection<string> UserNames { get; set; }
        public ICollection<string> RoleNames { get; set; }
        public IEnumerable<int> PreviousPages { get; set; }
        public IEnumerable<int> NextPages { get; set; }
        public ICollection<RoleRequestSorting> SortingOptions { get; set; }
        public IEnumerable<RoleRequestListModel> RoleRequests { get; set; }
    }
}
