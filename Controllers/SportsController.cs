using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAllSports()
        {
            var sports = await sportRepository.GetAllSports();
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

        [HttpGet]
        [Route("GetActiveSports")]
        public async Task<IActionResult> GetActiveSports()
        {
            var sports = await sportRepository.GetActiveSports();
            if (sports == null)
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

        [HttpPost]
        [Route("EditSport")]
        public async Task<IActionResult> EditSport(Sports Sport)
        {
            if (Sport == null)
            {
                return BadRequest("Sport data is null");
            }

            var EditedSport = await sportRepository.EditSport(Sport);
            if (EditedSport == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Sport Not Updated."
                };
                return Ok(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Sport Updated Successfully."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetSportById")]
        public async Task<IActionResult> GetSportById(Guid id)
        {
            Sports Sport = await sportRepository.GetSportById(id);
            if (Sport == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Sport Not Found."
                };
                return NotFound(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = Sport
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteSport")]
        public async Task<IActionResult> DeleteSport(Guid id)
        {
            Sports Sport = await sportRepository.GetSportById(id);
            if (Sport == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Tournament Not Found."
                };
                return NotFound(BadResponse);
            }
            else
            {
                await sportRepository.DeleteSport(id);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = null,
                Message = "Sport deleted Succesfully."
            };

            return Ok(response);
        }
    }
}
