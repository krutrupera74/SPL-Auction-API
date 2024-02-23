using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace auction.Repositories.Implementation
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TeamRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<TeamsList>> GetAllTeams()
        {
            return await dbContext.Teams
                        .OrderBy(x => x.Name)
            .GroupJoin(
                            dbContext.Tournaments,
                            team => team.TournamentId,
                            tournament => tournament.Id,
                            (team, tournament) => new
                            {
                                Team = team,
                                Tournament = tournament
                            })
                        .SelectMany(
                            x => x.Tournament.DefaultIfEmpty(),
                            (x, tournament) => new TeamsList
                            {
                                Id = x.Team.Id,
                                Name = x.Team.Name,
                                IsActive = x.Team.IsActive,
                                Tournament = tournament.Name
                            })
                        .ToListAsync();
        }
    }
}
