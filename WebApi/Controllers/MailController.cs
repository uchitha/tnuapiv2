﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Services;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

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

        [HttpPost]
        [Route("KidsCodeRegistration")]
        public async Task<IActionResult> KidsCodeRegistrationEmail([FromBody]KidsCodeRegistration model)
        {
            Trace.WriteLine("Registration info : ", JsonConvert.SerializeObject(model));
            var success = await _emailService.SendSingleEmail("kidscode@tnuit.com.au","Aus Kids Code - Registration", JsonConvert.SerializeObject(model) );
            if (!success) return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to send the notification. Can you please try again?");
            return Ok( new { status = success });
        }

        [HttpGet]
        [Route("Registration")]
        public async Task<IActionResult> RegistrationEmail([FromQuery]string name,
            [FromQuery]int age,
            [FromQuery]string parentsName,
            [FromQuery]string email,
            [FromQuery]string phone,
            [FromQuery]string course,
            [FromQuery]string comment)
        {
            var model = new KidsCodeRegistration
            {
                Name = name,
                Age = age,
                ParentsName = parentsName,
                Email = email,
                Phone = phone,
                Course = (KidsCodeCourse)Enum.Parse(typeof(KidsCodeCourse), course, true),
                Comment = comment
            };
            Trace.WriteLine("Registration info : ", JsonConvert.SerializeObject(model));
            var success = await _emailService.SendSingleEmail("kidscode@tnuit.com.au", "Aus Kids Code - Registration", JsonConvert.SerializeObject(model));
            if (!success) return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to send the notification. Can you please try again?");
            return Ok(new { status = success });
        }

        [HttpPost]
        [Route("test")]
        public IActionResult Test()
        {
            Debug.WriteLine($"Invoked test method");
            return Ok();
        }
    }
}