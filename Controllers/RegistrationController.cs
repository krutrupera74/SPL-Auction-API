using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> RegisterPlayer()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.GetFile("image");
            var name = formCollection["name"];
            var email = formCollection["email"];
            var employeeNo = int.Parse(formCollection["employeeNo"]);
            var gender = bool.Parse(formCollection["gender"]);
            var playerRating = int.Parse(formCollection["playerRating"]);
            var battingRating = int.Parse(formCollection["battingRating"]);
            var bowlingRating = int.Parse(formCollection["bowlingRating"]);
            var wicketKeepingRating = int.Parse(formCollection["wicketKeepingRating"]);
            var batsmanHand = formCollection["batsmanHand"];
            var bowlerHand = formCollection["bowlerHand"];
            var bowlingStyle = formCollection["bowlingStyle"];
            var interestedInCaptainOrOwner = bool.Parse(formCollection["interestedInCaptainOrOwner"]);
            var captainOrOwner = formCollection["captainOrOwner"];
            var comments = formCollection["comments"];
            var tournamentId = Guid.Parse(formCollection["tournamentId"]);
            var mobileNo = formCollection["mobileNo"];
            var playerAvailability = formCollection["playerAvailability"];

            if (formCollection.IsNullOrEmpty())
            {
                return BadRequest("Request is null");
            }

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
                Name = name,
                Gender = gender,
                comments = comments,
                PlayerRating = playerRating,
                TournamentId = tournamentId,
                BatsmanHand = batsmanHand,
                BowlerHand = bowlerHand,
                BowlingRating = bowlingRating,
                BattingRating = battingRating,
                BowlingStyle = bowlingStyle,
                CaptainOrOwner = captainOrOwner,
                Email = email,
                EmployeeNo = employeeNo,
                InterestedInCaptainOrOwner = interestedInCaptainOrOwner,
                WicketKeepingRating = wicketKeepingRating,
                ImagePath = imageUrl,
                PlayerAvailability = playerAvailability,
                Mobile = mobileNo
            };

            var addedPlayer = await playerRepository.AddPlayer(player);

            if (addedPlayer == null)
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
