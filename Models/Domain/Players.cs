namespace auction.Models.Domain
{
    public class Players
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int EmployeeNo { get; set; }
        public int PlayerRating { get; set; }
        public int BattingRating { get; set; }
        public int BowlingRating { get; set; }
        public int WicketKeepingRating { get; set; }
        public string Email { get; set; }
        public string comments { get; set; }
        public string CaptainOrOwner { get; set; }
        public string BatsmanHand { get; set; }
        public string BowlerHand { get; set; }
        public string BowlingStyle { get; set; }
        public Guid TournamentId { get; set; }
        public bool IsDuplicate { get; set; } = false;
        public bool InterestedInCaptainOrOwner { get; set; } = false;
        public string ImagePath { get; set; }
    }

    public class PlayersDTO
    {
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int EmployeeNo { get; set; }
        public int PlayerRating { get; set; }
        public int BattingRating { get; set; }
        public int BowlingRating { get; set; }
        public int WicketKeepingRating { get; set; }
        public string Email { get; set; }
        public string comments { get; set; }
        public string CaptainOrOwner { get; set; }
        public string BatsmanHand { get; set; }
        public string BowlerHand { get; set; }
        public string BowlingStyle { get; set; }
        public Guid TournamentId { get; set; }
        public bool InterestedInCaptainOrOwner { get; set; } = false;
        public string ImagePath { get; set; }
    }
}
