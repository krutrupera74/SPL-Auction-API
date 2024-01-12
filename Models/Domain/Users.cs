namespace auction.Models.Domain
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
    }

    public class UsersDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public Guid OrganizationId { get; set; }
    }

    public class UsersList
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string OrganizationName { get; set; }
    }
}
