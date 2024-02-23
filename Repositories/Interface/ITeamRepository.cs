using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface ITeamRepository
    {
        Task<List<TeamsList>> GetAllTeams();
    }
}
