using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface ITeamRepository
    {
        Task<List<TeamsList>> GetAllTeams();

        Task<Teams> AddTeam(Teams team);
        Task<Teams> EditTeam(Teams team);
        Task<Teams> GetTeamById(Guid id);
        Task DeleteTeam(Guid Id);
    }
}
