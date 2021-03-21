using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aks_12_factors_microservice.Model;
using aks_12_factors_microservice.Service;

namespace aks_12_factors_microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            if (ShutdownService.StopRequested) {
                throw new Exception();
            }
			return "OK";
        }
		
    }
}
