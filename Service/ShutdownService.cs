
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace aks_12_factors_microservice.Service
{
    public class ShutdownService: IHostedService
    {
        readonly ILogger _logger;
        readonly IHostApplicationLifetime _applicationLifetime;

        public static bool StopRequested {get; set; }

        public ShutdownService(IHostApplicationLifetime applicationLifetime, ILogger<ShutdownService> logger)
        {
            _applicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // register a callback that sleeps for 30 seconds
            _applicationLifetime.ApplicationStopping.Register(this.ApplicationStopping);
            _logger.LogInformation("Registered shutdown");
            return Task.CompletedTask;
        }

        public void ApplicationStopping() {
            _logger.LogInformation("Shutting down application after 30 seconds");
            StopRequested = true;
            Thread.Sleep(30000);
            _logger.LogInformation("Shutting down");
        }

        public Task StopAsync(CancellationToken cancellationToken)  {
            return Task.CompletedTask;
        }
    }

}