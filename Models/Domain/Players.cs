namespace auction.Models.Domain
{
    public class Players
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int PlayerRating { get; set; }
        public string comments { get; set; }
        public Guid TournamentId { get; set; }
        public bool IsDuplicate { get; set; } = false;
    }

    public class PlayersDTO
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int PlayerRating { get; set; }
        public string comments { get; set; }
        public Guid TournamentId { get; set; }
    }
}
