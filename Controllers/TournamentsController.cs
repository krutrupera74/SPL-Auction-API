using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static auction.Models.Enums.Enum;
using System.Data;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {

        public TournamentsController(ITournamentRepository tournamentRepository)
        {
            TournamentRepository = tournamentRepository;
        }

        public ITournamentRepository TournamentRepository { get; }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] TournamentsDTO TournamentsDTO)
        {
            if (TournamentsDTO == null)
            {
                return BadRequest("Tournament data is null");
            }

            var Tournament = new Tournaments
            {
                Name = TournamentsDTO.Name,
                StartDate = TournamentsDTO.StartDate,
                EndDate = TournamentsDTO.EndDate,
                IsActive = TournamentsDTO.IsActive,
                Description = TournamentsDTO.Description,
            };

            var AddedTournament = await TournamentRepository.AddTournament(Tournament);
            if (AddedTournament == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Tournament Not Added."
                };
                return BadRequest(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Tournament Added Successfully."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTournament")]
        public async Task<IActionResult> GetTournament(int Id)
        {
            var Tournament = await TournamentRepository.GetTournament(Id);

            if (Tournament == null)
            {
                return NotFound();
            }

            return Ok(Tournament);
        }
    }
}
