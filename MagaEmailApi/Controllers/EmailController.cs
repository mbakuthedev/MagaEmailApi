using MagaEmailApi.Models;
using MagaEmailApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagaEmailApi.Controllers
{
    
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _mailService;
        public EmailController(IEmailService mailService)
        {
            _mailService = mailService;
        }

        //TODO: Fix dependencies and extensions
        public async Task<IActionResult> SendEmail([FromBody]UserDetails userDetails)
        {
            try
            {
                if (uerDetails == null)
                {
                    return Problem(detail: "Invalid details");
                }
                else
                {
                    TransactionNotifications.SendComplaintByMail(this, userDetails);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                // Return error response
                return Problem(title: "SYSTEM ERROR",
                    statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message);
            }
            
        }
    }
}
