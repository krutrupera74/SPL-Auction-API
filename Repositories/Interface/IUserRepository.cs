using auction.Models.Domain;

namespace auction.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<Users> GetUserDataByUsername(string Username);
        Task<Users> AuthenticateUser(Users User, string Password);
        Task<List<UsersList>> GetAllUsers();
        Task<Users> AddUser(Users User);
        Task<Users> GetUserById(Guid id);
    }
}
