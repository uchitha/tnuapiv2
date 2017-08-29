using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Services;

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
        public async Task<IActionResult> KidsCodeRegistrationEmail(KidsCodeRegistration model)
        {
            await _emailService.SendSingleEmail(model.Email, "Aus Kids Code - Registration", "foo-bar");
            return Ok(new {status = _configuration.GetValue<string>("Emails:KidsCodeAdmin") });
        }
    }
}