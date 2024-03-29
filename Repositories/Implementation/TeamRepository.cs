﻿using auction.Data;
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

        public async Task<Teams> AddTeam(Teams team)
        {
            List<TeamsList> teamsList = await GetAllTeams();
            foreach (var tm in teamsList)
            {
                if (tm != null)
                {
                    if (team.Name == null || String.Compare(tm.Name, team.Name, true) == 0)
                    {
                        team.IsDuplicate = true;
                        return team;
                    }
                }
            }

            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            return team;
        }

        public async Task<Teams> EditTeam(Teams Team)
        {
            List<TeamsList> teamsList = await GetAllTeams();
            teamsList = teamsList.Where(x => x.Id != Team.Id).ToList();
            foreach (var tm in teamsList)
            {
                if (tm != null)
                {
                    if (Team.Name == null || String.Compare(tm.Name, Team.Name, true) == 0)
                    {
                        Team.IsDuplicate = true;
                        return Team;
                    }
                }
            }

            var existingTeam = await dbContext.Teams.Where(x => x.Id == Team.Id).FirstOrDefaultAsync();
            if (existingTeam != null)
            {
                existingTeam.Name = Team.Name;
                existingTeam.IsActive = Team.IsActive;
                existingTeam.ImagePath = Team.ImagePath;
                existingTeam.TournamentId = Team.TournamentId;
                await dbContext.SaveChangesAsync();
                return existingTeam;
            }
            return null;
        }

        public async Task<List<TeamsList>> GetAllTeams()
        {
            return await dbContext.Teams
                        .OrderBy(x => x.Name)
                        .Where(x => x.IsActive == true)
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
                                ImagePath = x.Team.ImagePath,
                                Tournament = tournament.Name
                            })
                        .ToListAsync();
        }

        public async Task<Teams> GetTeamById(Guid id)
        {
            return await dbContext.Teams.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteTeam(Guid Id)
        {
            var existingTeam = await dbContext.Teams.Where(x => x.Id == Id && x.IsActive).FirstOrDefaultAsync();
            if (existingTeam != null)
            {
                existingTeam.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
