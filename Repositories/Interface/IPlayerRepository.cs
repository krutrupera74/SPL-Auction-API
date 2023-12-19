using auction.Models.Domain;

namespace auction.Repositories.Interface
{
  public interface IPlayerRepository
  {
    Task<Players> AddPlayer(Players player);
  }
}
