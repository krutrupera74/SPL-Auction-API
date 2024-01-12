using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auction.Repositories.Implementation
{
    public class SportRepository : ISportRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SportRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Sports> AddSport([FromBody]Sports sport)
        {
            await dbContext.Sports.AddAsync(sport);
            await dbContext.SaveChangesAsync();
            return sport;
        }

        public async Task<List<Sports>> GetAllSports(Guid OrganizationId)
        {
            return await dbContext.Sports.Where(x => x.IsActive && x.OrganizationId == OrganizationId).ToListAsync();
        }
    }
}
