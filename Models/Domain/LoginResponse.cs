namespace auction.Models.Domain
{
    public class LoginResponse
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
