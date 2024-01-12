using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace auction.Repositories.Implementation
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrganizationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<(Organization organization, bool isDuplicate)> AddOrganization(Organization organization)
        {
            // Check if an organization with the same name already exists
            bool isDuplicate = dbContext.Organizations.Any(o => o.OrganizationName == organization.OrganizationName);

            if (isDuplicate)
            {
                // Organization with the same name already exists
                // You can return an appropriate message or throw an exception
                // For example, return null with a message indicating the duplicate name
                return (null, true);
            }

            // No organization with the same name found, proceed with adding the new organization
            await dbContext.Organizations.AddAsync(organization);
            await dbContext.SaveChangesAsync();

            return (organization, false);
        }

        public async Task<Organization> EditOrganization(Organization organization)
        {
            var existingOrganization = await dbContext.Organizations.Where(x => x.Id == organization.Id).FirstOrDefaultAsync();
            if(existingOrganization != null)
            {
                existingOrganization.OrganizationName = organization.OrganizationName;
                existingOrganization.IsActive = organization.IsActive;
                await dbContext.SaveChangesAsync();
                return existingOrganization;
            }
            return null;
        }

        public async Task<List<Organization>> GetAllOrganizations()
        {
            return await dbContext.Organizations.OrderBy(o => o.OrganizationName).ToListAsync();
        }

        public async Task<List<Organization>> GetActiveOrganizations()
        {
            return await dbContext.Organizations.Where(x => x.IsActive).OrderBy(o => o.OrganizationName).ToListAsync();
        }

        public async Task<Organization> GetOrganizationById(Guid id)
        {
            return await dbContext.Organizations.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
