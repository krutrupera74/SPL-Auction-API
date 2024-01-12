using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface IOrganizationRepository
    {
        Task<(Organization organization, bool isDuplicate)> AddOrganization(Organization organization);
        Task<Organization> EditOrganization(Organization organization);
        Task<List<Organization>> GetAllOrganizations();
        Task<List<Organization>> GetActiveOrganizations();
        Task<Organization> GetOrganizationById(Guid id);
    }
}
