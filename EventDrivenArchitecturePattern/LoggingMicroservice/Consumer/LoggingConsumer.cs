using MassTransit;
using Domain;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LoggingMicroservice.Consumer
{
    public class LoggingConsumer : IConsumer<LogMessage>
    {
        private readonly ILogger<LoggingConsumer> _logger;

        public LoggingConsumer(ILogger<LoggingConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<LogMessage> context)
        {
            this._logger.LogInformation(context.Message.Description);
            return Task.CompletedTask;
        }
    }
}
