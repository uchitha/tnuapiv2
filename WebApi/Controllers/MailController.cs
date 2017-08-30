using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Services;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private IConfiguration _configuration;
        private IEmailService _emailService;

        public MailController(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }

        [Route("KidsCodeRegistration")]
        [EnableCors("AllowSpecificOrigin")]
        public async Task<IActionResult> KidsCodeRegistrationEmail([FromBody]KidsCodeRegistration model)
        {
            Debug.WriteLine($"Sending Kids Code Registration email to : {model.Email}");
            var success = await _emailService.SendSingleEmail(model.Email, "Aus Kids Code - Registration", "foo-bar");
            Debug.Flush();
            return Ok(new { success });

        }
    }
}