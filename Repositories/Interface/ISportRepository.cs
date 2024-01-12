using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface ISportRepository
    {
        Task<List<Sports>> GetAllSports(Guid OrganizationId);
        Task<Sports> AddSport(Sports sport);
    }
}
