using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface ITournamentRepository
    {
        Task<Tournaments> AddTournament(Tournaments Tournament);
        Task<Tournaments> EditTournament(Tournaments Tournament);
        Task<Tournaments> GetTournament(Guid Id);

        Task<List<TournamentList>> GetAllTournaments();

        Task<List<Tournaments>> GetActiveTournaments();
        Task<Tournaments> GetTournamentById(Guid id);
        Task<Tournaments> ValidateTournament(Guid id);
    }
}
