using auction.Helpers;
using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static auction.Models.Domain.FileUploadModel;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamRepository teamRepository;
        private readonly IFileUploadHelper fileUploadHelper;

        public TeamsController(ITeamRepository teamRepository, IFileUploadHelper fileUploadHelper)
        {
            this.teamRepository = teamRepository;
            this.fileUploadHelper = fileUploadHelper;
        }
        [HttpGet]
        [Route("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            var Teams = await teamRepository.GetAllTeams();

            if (Teams == null)
            {
                return NotFound();
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "",
                Data = Teams
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("AddTeam")]
        public async Task<IActionResult> AddTeam()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.GetFile("image");
            var name = formCollection["name"];
            var tournamentId = Guid.Parse(formCollection["tournamentId"]);
            var isActive = bool.Parse(formCollection["isActive"]);

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            if (file.ContentType.ToLower() != "image/jpeg" && file.ContentType.ToLower() != "image/png")
            {
                return BadRequest("Only JPEG and PNG images are allowed");
            }

            var imageUrl = await fileUploadHelper.UploadImage(file);

            var Team = new Teams
            {
                Name = name,
                TournamentId = tournamentId,
                IsActive = isActive,
                ImagePath = imageUrl
            };

            var AddedTeam = await teamRepository.AddTeam(Team);

            if (AddedTeam == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Team Not Added."
                };
                return BadRequest(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Team Added Successfully."
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("EditTeam")]
        public async Task<IActionResult> EditTeam(Teams Team)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.GetFile("image");
            var imageUrl = formCollection["imageUrl"];
            var name = formCollection["name"];
            var tournamentId = Guid.Parse(formCollection["tournamentId"]);
            var isActive = bool.Parse(formCollection["isActive"]);

            var existingTeam = await teamRepository.GetTeamById(Team.Id);
            if (existingTeam == null)
            {
                return NotFound("Team not found");
            }

            if (file != null && file.Length > 0)
            {
                if (file.ContentType.ToLower() != "image/jpeg" && file.ContentType.ToLower() != "image/png")
                {
                    return BadRequest("Only JPEG and PNG images are allowed");
                }

                if (existingTeam.ImagePath != imageUrl)
                {
                    var newImageUrl = await fileUploadHelper.UploadImage(file);
                    existingTeam.ImagePath = newImageUrl;
                }
            }

            existingTeam.Name = name;
            existingTeam.TournamentId = tournamentId;
            existingTeam.IsActive = isActive;

            var updatedTeam = await teamRepository.EditTeam(existingTeam);

            if (updatedTeam == null)
            {
                var badResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Team not updated."
                };
                return BadRequest(badResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Team updated successfully."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTeamById")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            Teams Team = await teamRepository.GetTeamById(id);
            if (Team == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Team Not Found."
                };
                return NotFound(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = Team
            };

            return Ok(response);
        }
    }
}
