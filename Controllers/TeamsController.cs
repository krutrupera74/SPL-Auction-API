using auction.Models.Domain;
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
        private readonly IFileUploadHelper _ifileUploadHelper;

        public TeamsController(ITeamRepository teamRepository, IFileUploadHelper _ifileUploadHelper)
        {
            this.teamRepository = teamRepository;
            this._ifileUploadHelper = _ifileUploadHelper;
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
        public async Task<IActionResult> AddTeam([FromForm] TeamsDTO teamsDTO)
        {
            if (teamsDTO.Image == null || teamsDTO.Image.Length == 0)
                return BadRequest("Image is required");

            // Upload image to Blob Storage
            var imageUrl = await _ifileUploadHelper.UploadImageToBlobStorage(teamsDTO.Image);



            return Ok("Team uploaded successfully");
        }
    }
}
