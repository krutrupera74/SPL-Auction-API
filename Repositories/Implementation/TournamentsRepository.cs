using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace auction.Repositories.Implementation
{
    public class TournamentsRepository : ITournamentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TournamentsRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Tournaments> AddTournament([FromBody] Tournaments Tournament)
        {
            await dbContext.Tournaments.AddAsync(Tournament);
            await dbContext.SaveChangesAsync();
            return Tournament;
        }

        public async Task<Tournaments> GetTournament(int Id)
        {
            return await dbContext.Tournaments.FirstOrDefaultAsync(s => s.Id == Id && s.IsActive);
        }
    }
}
