using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            this.dashboardRepository = dashboardRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetDashboardData")]
        public async Task<IActionResult> GetDashboardData()
        {
            DashboardResponseModel data = await dashboardRepository.GetDashboardData();
            if(data == null)
            {
                var BadResponse = new ResponseModel
                {
                    Success = false,
                    Message = "Data Not Found"
                };
                return Ok(BadResponse);
            }

            var response = new ResponseModel
            {
                Success = true,
                Data = data,
                Message = "Data Found"
            };
            return Ok(response);
        }
    }
}
