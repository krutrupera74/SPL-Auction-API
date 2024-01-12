using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;

        public RegistrationController(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterPlayer([FromBody] Players request)
        {
            var player = new Players
            {
                Name = request.Name,
                Gender = request.Gender,
                BattingRating = request.BattingRating,
                BowlingRating = request.BowlingRating,
                WicketKeepingRating = request.WicketKeepingRating,
                comment = request.comment
            };

            player = await playerRepository.AddPlayer(player);

            var response = new Players
            {
                Name = player.Name,
                Gender = player.Gender,
                BattingRating = player.BattingRating,
                BowlingRating = player.BowlingRating,
                WicketKeepingRating = player.WicketKeepingRating,
                comment = player.comment
            };

            return Ok(response);
        }
    }
}
