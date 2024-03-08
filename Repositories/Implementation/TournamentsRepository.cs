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

            List<TournamentList> tournamentList = await GetAllTournaments();
            List<Sports> sports = await dbContext.Sports.Where(x => x.Id == Tournament.SportId).ToListAsync();
            foreach (var tm in tournamentList)
            {
                if (tm != null)
                {
                    if (Tournament.Name == null || String.Compare(tm.Name, Tournament.Name, true) == 0)
                    {
                        if (sports != null)
                        {
                            foreach (var sp in sports)
                            {
                                if (String.Compare(sp.Name, tm.Sport, true) == 0)
                                {
                                    Tournament.IsDuplicate = true;
                                    return Tournament;
                                }
                            }
                        }
                    }
                }
            }
            await dbContext.Tournaments.AddAsync(Tournament);
            await dbContext.SaveChangesAsync();
            return Tournament;
        }

        public async Task<List<TournamentList>> GetAllTournaments()
        {
            return await dbContext.Tournaments
                        .OrderBy(x => x.Name)
                        .Where(x => x.IsActive == true)
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
                                Sport = sport.Name,
                                Venue=x.Tournament.Venue,
                                TournamentDates=x.Tournament.TournamentDates
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

        public async Task<Tournaments> GetTournamentById(Guid id)
        {
            return await dbContext.Tournaments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Tournaments> EditTournament(Tournaments Tournament)
        {
            List<TournamentList> tournamentList = await GetAllTournaments();
            List<Sports> sports = await dbContext.Sports.Where(x => x.Id == Tournament.SportId).ToListAsync();
            tournamentList = tournamentList.Where(x => x.Id != Tournament.Id).ToList();
            foreach (var tm in tournamentList)
            {
                if (tm != null)
                {
                    if (Tournament.Name == null || String.Compare(tm.Name, Tournament.Name, true) == 0)
                    {
                        if (sports != null)
                        {
                            foreach (var sp in sports)
                            {
                                if (String.Compare(sp.Name, tm.Sport, true) == 0)
                                {
                                    Tournament.IsDuplicate = true;
                                    return Tournament;
                                }
                            }
                        }
                    }
                }
            }
            var existingTournament = await dbContext.Tournaments.Where(x => x.Id == Tournament.Id).FirstOrDefaultAsync();
            if (existingTournament != null)
            {
                existingTournament.Name = Tournament.Name;
                existingTournament.IsActive = Tournament.IsActive;
                existingTournament.StartDate = Tournament.StartDate;
                existingTournament.EndDate = Tournament.EndDate;
                existingTournament.Description = Tournament.Description;
                existingTournament.SportId = Tournament.SportId;
                existingTournament.Venue = Tournament.Venue;
                existingTournament.TournamentDates = Tournament.TournamentDates;
                await dbContext.SaveChangesAsync();
                return existingTournament;
            }
            return null;
        }

        public async Task<ValidateTournament> ValidateTournament(Guid id)
        {
            return await dbContext.Tournaments.Where(x => x.Id == id && x.IsActive)
                .Join(dbContext.Sports,
                tournament => tournament.SportId,
                sport => sport.Id,
                (tournament, sport) => new { Tournament = tournament, SportName = sport.Name })
                .Select(result => new ValidateTournament
                {
                    Id = result.Tournament.Id,
                    Name = result.Tournament.Name,
                    SportId = result.Tournament.SportId,
                    Description = result.Tournament.Description,
                    EndDate = result.Tournament.EndDate,
                    IsActive = result.Tournament.IsActive,
                    IsDuplicate = result.Tournament.IsDuplicate,
                    StartDate = result.Tournament.StartDate,
                    Venue = result.Tournament.Venue,
                    TournamentDates = result.Tournament.TournamentDates,
                    IsCricket = result.SportName.Trim().Equals("cricket", StringComparison.OrdinalIgnoreCase)                    
                })
                .FirstOrDefaultAsync();
        }

        public async Task DeleteTournament(Guid Id)
        {
            var existingTournament = await dbContext.Tournaments.Where(x => x.Id == Id && x.IsActive).FirstOrDefaultAsync();
            if (existingTournament != null)
            {
                existingTournament.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsTournamentExistInTeam(Guid id)
        {
            var result = await dbContext.Teams.Where(x => x.TournamentId == id && x.IsActive).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
