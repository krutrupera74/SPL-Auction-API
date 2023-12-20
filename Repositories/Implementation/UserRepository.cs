using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace auction.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Users> AuthenticateUser(Users User, string Password)
        {
            var UserModel = dbContext.Users.AsNoTracking().FirstOrDefault(x => x.Password == Password && x.IsActive);

            if (UserModel == null)
            {
                return null;
            }
            else
            {
                return UserModel;
            }
        }

        public async Task<Users> GetUserDataByUsername(string username)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
