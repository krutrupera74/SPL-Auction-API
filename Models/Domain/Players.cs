namespace auction.Models.Domain
{
    public class Players
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Gender { get; set; }
        public int BattingRating { get; set; }
        public int BowlingRating { get; set; }
        public int WicketKeepingRating { get; set; }
        public string comment { get; set; }
        public int TeamId { get; set; }
        public bool IsActive { get; set; }
    }
}
