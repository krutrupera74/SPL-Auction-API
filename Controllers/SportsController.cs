using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static auction.Models.Enums.Enum;
using System.Data;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ISportRepository sportRepository;

        public SportsController(ISportRepository sportRepository)
        {
            this.sportRepository = sportRepository;
        }

        [HttpGet]
        [Route("GetAllSports")]
        public async Task<IActionResult> GetAllSports(Guid OrganizationId)
        {
            var sports = sportRepository.GetAllSports(OrganizationId);
            if(sports == null)
            {
                return NotFound();
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = sports
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("AddSport")]
        public async Task<IActionResult> AddSport(SportsDTO sportDTO)
        {
            if(sportDTO == null)
            {
                return BadRequest("Sport Data Is Null");
            }

            var Sport = new Sports
            {
                Name = sportDTO.Name,
                IsActive = sportDTO.IsActive,
                OrganizationId = sportDTO.OrganizationId
            };

            var AddedSport = await sportRepository.AddSport(Sport);
            if (AddedSport == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Sport Not Added."
                };
                return BadRequest(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Sport Added Successfully."
            };

            return Ok(response);
        }
    }
}
