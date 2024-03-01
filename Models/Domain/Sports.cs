namespace auction.Models.Domain
{
    public class Sports
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
        public bool IsDuplicate { get; set; } = false;
    }

    public class SportsDTO
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
    }

    public class SportsList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Organization { get; set; }
    }
}
