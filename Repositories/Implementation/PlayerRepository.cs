using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;

namespace auction.Repositories.Implementation
{
  public class PlayerRepository : IPlayerRepository
  {
    private readonly ApplicationDbContext dbContext;

    public PlayerRepository(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public async Task<Players> AddPlayer(Players player)
    {
      await dbContext.Players.AddAsync(player);
      await dbContext.SaveChangesAsync();
      return player;
    }
  }
}
