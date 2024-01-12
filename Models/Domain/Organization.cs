namespace auction.Models.Domain
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public bool IsActive { get; set; }
    }

    public class OrganizationDTO
    {
        public string OrganizationName { get; set; }
        public bool IsActive { get; set; }
    }
}
