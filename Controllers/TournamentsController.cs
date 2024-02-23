﻿using auction.Data;
using auction.Models.Domain;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static auction.Models.Enums.Enum;
using System.Data;
using auction.Repositories.Implementation;

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
                SportId = TournamentsDTO.SportId,
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

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> EditTournament(Tournaments Tournament)
        {
            if (Tournament == null)
            {
                return BadRequest("Tournament data is null");
            }

            var EditedTournament = await TournamentRepository.EditTournament(Tournament);
            if (EditedTournament == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Tournament Not Updated."
                };
                return Ok(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Tournament Updated Successfully."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTournament")]
        public async Task<IActionResult> GetTournament(Guid Id)
        {
            var Tournament = await TournamentRepository.GetTournament(Id);

            if (Tournament == null)
            {
                return NotFound();
            }
            var response = new ResponseModel
            {
                Success = true,
                Message = "",
                Data = Tournament
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllTournaments")]
        public async Task<IActionResult> GetAllTournaments()
        {
            var Tournaments = await TournamentRepository.GetAllTournaments();

            if (Tournaments == null)
            {
                return NotFound();
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "",
                Data = Tournaments
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetActiveTournaments")]
        public async Task<IActionResult> GetActiveTournaments()
        {
            List<Tournaments> Tournaments = await TournamentRepository.GetActiveTournaments();
            var response = new ResponseModel
            {
                Success = true,
                Data = Tournaments
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GeTournamentById")]
        public async Task<IActionResult> GeTournamentById(Guid id)
        {
            Tournaments Tournament = await TournamentRepository.GeTournamentById(id);
            if (Tournament == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Tournament Not Found."
                };
                return NotFound(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = Tournament
            };

            return Ok(response);
        }
    }
}
