using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddOrganization(OrganizationDTO organizationDTO)
        {
            if (organizationDTO == null)
            {
                return BadRequest("Organization data is null");
            }

            var organization = new Organization
            {
                OrganizationName = organizationDTO.OrganizationName,
                IsActive = organizationDTO.IsActive
            };

            var (addedOrganization, isDuplicate) = await organizationRepository.AddOrganization(organization);
            if (isDuplicate)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Organization Already Exists."
                };
                return Ok(BadResponse);
            }
            if (addedOrganization == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Organization Not Added."
                };
                return Ok(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Organization Added Successfully."
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> EditOrganization(Organization organization)
        {
            if (organization == null)
            {
                return BadRequest("Organization data is null");
            }

            var EditedOrganization = await organizationRepository.EditOrganization(organization);
            if (EditedOrganization == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Organization Not Updated."
                };
                return Ok(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "Organization Updated Successfully."
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllOrganizations")]
        public async Task<IActionResult> GetAllOrganization()
        {
            List<Organization> organizations = await organizationRepository.GetAllOrganizations();
            var response = new ResponseModel
            {
                Success = true,
                Data = organizations
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetActiveOrganizations")]
        public async Task<IActionResult> GetActiveOrganizations()
        {
            List<Organization> organizations = await organizationRepository.GetActiveOrganizations();
            var response = new ResponseModel
            {
                Success = true,
                Data = organizations
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetOrganizationById")]
        public async Task<IActionResult> GetOrganizationById(Guid id)
        {
            Organization organization = await organizationRepository.GetOrganizationById(id);
            if(organization == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Organization Not Found."
                };
                return NotFound(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = organization
            };

            return Ok(response);
        }
    }
}
