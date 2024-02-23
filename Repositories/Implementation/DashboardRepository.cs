using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace auction.Repositories.Implementation
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DashboardRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DashboardResponseModel> GetDashboardData()
        {
            DashboardResponseModel responseModel = new DashboardResponseModel();
            responseModel.UsersCount = await dbContext.Users.Where(x => x.IsActive && x.Role != "Admin").CountAsync();
            responseModel.OrganizationsCount = await dbContext.Organizations.Where(x => x.IsActive).CountAsync();
            responseModel.TournamentsCount = await dbContext.Tournaments.Where(x => x.IsActive).CountAsync();
            responseModel.PlayersCount = await dbContext.Players.CountAsync();
            responseModel.SportsCount = await dbContext.Sports.Where(x => x.IsActive).CountAsync();
            responseModel.TeamsCount = await dbContext.Teams.Where(x => x.IsActive).CountAsync();

            return responseModel;
        }

        public async Task<List<Tournaments>> GetTournaments()
        {
            return await dbContext.Tournaments.Where(x => x.IsActive).ToListAsync();
        }
    }
}
