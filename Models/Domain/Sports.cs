namespace auction.Models.Domain
{
    public class Sports
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
    }

    public class SportsDTO
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
