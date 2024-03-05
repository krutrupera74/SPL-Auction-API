using auction.Helpers;
using auction.Models.Domain;
using auction.Repositories.Implementation;
using auction.Repositories.Interface;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Pkcs;

namespace auction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailRepository emailRepository;

        public EmailController(IEmailRepository _emailRepository)
        {
            this.emailRepository = _emailRepository;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await emailRepository.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
