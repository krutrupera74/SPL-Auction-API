namespace auction.Models.Domain
{
    public class Tournaments
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public Guid SportId { get; set; }
        public bool IsDuplicate { get; set; } = false;
    }

    public class TournamentsDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public Guid SportId { get; set; }
    }

    public class TournamentList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Sport { get; set; }
    }
}
