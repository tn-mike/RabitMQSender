using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabitMQSender.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RabitMQSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISendService _service;

        public SenderController(ISendService service)
        {
            _service = service;
        }

        [HttpGet]
        public string SendMessage()
        {
            return _service.SendMessage();
        }
    }
}
