using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static auction.Models.Enums.Enum;
using System.Data;
using auction.Models.Enums;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userRepository.GetAllUsers();
            var response = new ResponseModel
            {
                Success = true,
                Data = users
            };

            return Ok(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUser([FromBody] UsersDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("User data is null");
            }

            var User = new Users
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Role = userDTO.Role,
                OrganizationId = userDTO.OrganizationId,
                IsActive = userDTO.IsActive
            };

            var AddedUser = await userRepository.AddUser(User);
            if (AddedUser == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "User Not Added."
                };
                return BadRequest(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Message = "User Added Successfully."
            };

            return Ok(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetOrganizationById(Guid id)
        {
            Users user = await userRepository.GetUserById(id);
            if (user == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "User Not Found."
                };
                return NotFound(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = user
            };

            return Ok(response);
        }
    }
}
