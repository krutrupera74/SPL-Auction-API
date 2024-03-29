﻿using auction.Data;
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

        public async Task<Sports> AddSport([FromBody] Sports sport)
        {
            List<SportsList> sports = await GetAllSports();
            foreach (var sp in sports)
            {
                if (sp != null)
                {
                    if (sport.Name == null || String.Compare(sp.Name, sport.Name, true) == 0)
                    {
                        sport.IsDuplicate = true;
                        return sport;
                    }                        
                }
            }
            await dbContext.Sports.AddAsync(sport);
            await dbContext.SaveChangesAsync();
            return sport;
        }

        public async Task<Sports> GetSportById(Guid id)
        {
            return await dbContext.Sports.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Sports>> GetActiveSports()
        {
            return await dbContext.Sports.OrderBy(x => x.Name).Where(x => x.IsActive).ToListAsync();
        }

        public async Task<List<SportsList>> GetAllSports()
        {
            return await dbContext.Sports.OrderBy(x => x.Name)
                         .Where(x => x.IsActive == true)
                         .GroupJoin(
                        dbContext.Organizations,
                        sport => sport.OrganizationId,
                        organization => organization.Id,
                        (sport, organization) => new
                        {
                            Sports = sport,
                            Organization = organization
                        })
                         .SelectMany(
                        x => x.Organization.DefaultIfEmpty(),
                        (x, organization) => new SportsList
                        {
                            Id = x.Sports.Id,
                            IsActive = x.Sports.IsActive,
                            Name = x.Sports.Name,
                            Organization = organization.OrganizationName
                        })
                         .ToListAsync();
        }

        public async Task<Sports> GetSportByOrganizationId(Guid OrganizationId)
        {
            return await dbContext.Sports.Where(x => x.IsActive && x.OrganizationId == OrganizationId).FirstOrDefaultAsync();
        }

        public async Task<Sports> EditSport(Sports Sport)
        {
            List<SportsList> sportsList = await GetAllSports();
            sportsList = sportsList.Where(x => x.Id != Sport.Id).ToList();
            foreach (var sp in sportsList)
            {
                if (sp != null)
                {
                    if (Sport.Name == null || String.Compare(sp.Name, Sport.Name, true) == 0)
                    {
                        Sport.IsDuplicate = true;
                        return Sport;
                    }
                }
            }
            var existingSport = await dbContext.Sports.Where(x => x.Id == Sport.Id).FirstOrDefaultAsync();
            if (existingSport != null)
            {
                existingSport.Name = Sport.Name;
                existingSport.IsActive = Sport.IsActive;
                existingSport.OrganizationId = Sport.OrganizationId;
                await dbContext.SaveChangesAsync();
                return existingSport;
            }
            return null;
        }

        public async Task DeleteSport(Guid Id)
        {
            var existingSport = await dbContext.Sports.Where(x => x.Id == Id && x.IsActive).FirstOrDefaultAsync();
            if (existingSport != null)
            {
                existingSport.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> IsSportExistInTournament(Guid id)
        {
            var result = await dbContext.Tournaments.Where(x => x.SportId == id && x.IsActive).FirstOrDefaultAsync();
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
