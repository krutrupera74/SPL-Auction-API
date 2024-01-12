namespace auction.Models.Domain
{
    public class Teams
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int TournamentId { get; set; }
        public bool IsActive { get; set; }
    }

    public class TeamsDTO
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public int TournamentId { get; set; }
        public bool IsActive { get; set; }
    }
}
