namespace auction.Models.Domain
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class DashboardResponseModel
    {
        public int UsersCount { get; set; }
        public int OrganizationsCount { get; set; }
        public int TournamentsCount { get; set; }
        public int PlayersCount { get; set; }
    }
}
