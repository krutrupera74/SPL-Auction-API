using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface IDashboardRepository
    {
        Task<List<Tournaments>> GetTournaments();
        Task<DashboardResponseModel> GetDashboardData();
    }
}
