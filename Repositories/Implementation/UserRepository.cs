using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace auction.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Users> AddUser(Users User)
        {
            await dbContext.Users.AddAsync(User);
            await dbContext.SaveChangesAsync();
            return User;
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

        public async Task<List<UsersList>> GetAllUsers()
        {
            var users = await dbContext.Users
                .AsNoTracking()
                .Where(x => x.Role != "Admin")
                .Join(dbContext.Organizations,
                user => user.OrganizationId,
                organization => organization.Id,
                (user, organization) =>
                new UsersList
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    OrganizationName = organization.OrganizationName
                }).ToListAsync();
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public async Task<Users> GetUserById(Guid id)
        {
            return await dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserDataByUsername(string username)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
