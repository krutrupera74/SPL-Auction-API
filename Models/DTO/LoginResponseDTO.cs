namespace auction.Models.DTO
{
    public class LoginResponseDTO
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
