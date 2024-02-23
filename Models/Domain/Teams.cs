﻿namespace auction.Models.Domain
{
    public class Teams
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public Guid TournamentId { get; set; }
        public bool IsActive { get; set; }
    }

    public class TeamsDTO
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public Guid TournamentId { get; set; }
        public bool IsActive { get; set; }
    }

    public class TeamsList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Tournament { get; set; }
        public bool IsActive { get; set; }
    }
}
