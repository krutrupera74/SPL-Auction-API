using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface ISportRepository
    {
        Task<Sports> GetSportByOrganizationId(Guid OrganizationId);
        Task<List<SportsList>> GetAllSports();
        Task<List<Sports>> GetActiveSports();
        Task<Sports> AddSport(Sports sport);
        Task<Sports> EditSport(Sports sport);
        Task<Sports> GetSportById(Guid id);
        Task DeleteSport(Guid Id);
    }
}
