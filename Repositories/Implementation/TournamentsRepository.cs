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

        public async Task<List<TournamentList>> GetAllTournaments()
        {
            return await dbContext.Tournaments
                        .OrderBy(x => x.Name)
                        .GroupJoin(
                            dbContext.Sports,
                            tournament => tournament.SportId,
                            sport => sport.Id,
                            (tournament, sportGroup) => new
                            {
                                Tournament = tournament,
                                Sports = sportGroup
                            })
                        .SelectMany(
                            x => x.Sports.DefaultIfEmpty(),
                            (x, sport) => new TournamentList
                            {
                                Id = x.Tournament.Id,
                                Name = x.Tournament.Name,
                                Description = x.Tournament.Description,
                                EndDate = x.Tournament.EndDate,
                                IsActive = x.Tournament.IsActive,
                                StartDate = x.Tournament.StartDate,
                                Sport = sport.Name
                            })
                        .ToListAsync();
        }

        public async Task<List<Tournaments>> GetActiveTournaments()
        {
            return await dbContext.Tournaments.Where(x => x.IsActive).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Tournaments> GetTournament(Guid Id)
        {
            return await dbContext.Tournaments.FirstOrDefaultAsync(s => s.Id == Id && s.IsActive);
        }

        public async Task<Tournaments> GeTournamentById(Guid id)
        {
            return await dbContext.Tournaments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Tournaments> EditTournament(Tournaments Tournament)
        {
            var existingTournament = await dbContext.Tournaments.Where(x => x.Id == Tournament.Id).FirstOrDefaultAsync();
            if (existingTournament != null)
            {
                existingTournament.Name = Tournament.Name;
                existingTournament.IsActive = Tournament.IsActive;
                existingTournament.StartDate = Tournament.StartDate;
                existingTournament.EndDate = Tournament.EndDate;
                existingTournament.Description = Tournament.Description;
                existingTournament.SportId = Tournament.SportId;
                await dbContext.SaveChangesAsync();
                return existingTournament;
            }
            return null;
        }

        public async Task<Tournaments> ValidateTournament(Guid id)
        {
            return await dbContext.Tournaments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
