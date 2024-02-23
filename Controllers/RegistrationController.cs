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
        private readonly ITournamentRepository tournamentRepository;

        public RegistrationController(IPlayerRepository playerRepository, ITournamentRepository tournamentRepository)
        {
            this.playerRepository = playerRepository;
            this.tournamentRepository = tournamentRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterPlayer")]
        public async Task<IActionResult> RegisterPlayer([FromBody] PlayersDTO request)
        {
            if(request == null)
            {
                return BadRequest("Player details are null.");
            }

            Players player = new Players()
            {
                Name = request.Name,
                Gender = request.Gender,
                comments = request.comments,
                PlayerRating = request.PlayerRating,
                TournamentId = request.TournamentId
            };

            var addedPlayer = await playerRepository.AddPlayer(player);

            if(addedPlayer == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Player Not Added."
                };
                return Ok(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Registered Successfully."
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ValidateTournament")]
        public async Task<IActionResult> ValidateTournament(string id)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                // Parsing succeeded, 'guid' contains the parsed GUID
                var Tournament = await tournamentRepository.ValidateTournament(guid);

                if (Tournament == null)
                {
                    var NotFoundResponse = new ResponseModel
                    {
                        Success = false,
                        Message = "Tournament not found.",
                        Data = null
                    };

                    return Ok(NotFoundResponse);
                }

                var response = new ResponseModel
                {
                    Success = true,
                    Message = "Tournament found.",
                    Data = Tournament
                };

                return Ok(response);
            }
            else
            {
                var NotFoundResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Tournament not found.",
                    Data = null
                };

                return Ok(NotFoundResponse);
            }
        }
    }
}
