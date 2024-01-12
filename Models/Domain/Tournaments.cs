namespace auction.Models.Domain
{
    public class Tournaments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int SportId { get; set; }
    }

    public class TournamentsDTO
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public int SportId { get; set; }
    }
}
