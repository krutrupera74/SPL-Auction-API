using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;
        private readonly ITournamentRepository tournamentRepository;
        private readonly IFileUploadHelper fileUploadHelper;

        public RegistrationController(IPlayerRepository playerRepository, ITournamentRepository tournamentRepository, IFileUploadHelper fileUploadHelper)
        {
            this.playerRepository = playerRepository;
            this.tournamentRepository = tournamentRepository;
            this.fileUploadHelper = fileUploadHelper;
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

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.GetFile("image");

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            if (file.ContentType.ToLower() != "image/jpeg" && file.ContentType.ToLower() != "image/png")
            {
                return BadRequest("Only JPEG and PNG images are allowed");
            }

            var imageUrl = await fileUploadHelper.UploadImage(file, "player_images");

            Players player = new Players()
            {
                Name = request.Name,
                Gender = request.Gender,
                comments = request.comments,
                PlayerRating = request.PlayerRating,
                TournamentId = request.TournamentId,
                BatsmanHand = request.BatsmanHand,
                BowlerHand = request.BowlerHand,
                BowlingRating = request.BowlingRating,
                BowlingStyle = request.BowlingStyle,
                CaptainOrOwner = request.CaptainOrOwner,
                Email = request.Email,
                EmployeeNo = request.EmployeeNo,
                InterestedInCaptainOrOwner = request.InterestedInCaptainOrOwner,
                WicketKeepingRating = request.WicketKeepingRating,
                PlayerAvailability = request.PlayerAvailability,
                IsMarquee = request.IsMarquee,
                Mobile = request.Mobile,
                ImagePath = imageUrl
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

                    return Unauthorized(NotFoundResponse);
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

                return Unauthorized(NotFoundResponse);
            }
        }
    }
}
