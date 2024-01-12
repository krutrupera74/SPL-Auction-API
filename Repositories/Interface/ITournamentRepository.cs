using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface ITournamentRepository
    {
        Task<Tournaments> AddTournament(Tournaments Tournament);
        Task<Tournaments> GetTournament(int Id);
    }
}
