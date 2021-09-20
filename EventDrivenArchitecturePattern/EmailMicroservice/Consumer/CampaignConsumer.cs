using Domain;
using EmailMicroservice.DataTransferObject;
using EmailMicroservice.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace EmailMicroservice.Consumer
{
    public class CampaignConsumer : IConsumer<Campaign>
    {
        private readonly IMailService _mailService;
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public CampaignConsumer(IMailService mailService, IBus bus, IConfiguration configuration)
        {
            _mailService = mailService;
            _bus = bus;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<Campaign> context)
        {
            var campaign = context.Message;

            MailRequest mailRequest = new MailRequest
            {
                ToEmail = "topx777@gmail.com",
                Subject = "Campaña Creada",
                Body = "Se creo la campaña" + campaign.Name,
            };

            await _mailService.SendEmailAsync(mailRequest);

            Uri logUri = new Uri($"{_configuration.GetConnectionString("rabitmq")}/LogQueue");
            var logEndPoint = await _bus.GetSendEndpoint(logUri);
            var logEntity = new LogMessage() { Description = $"Message to: {mailRequest.ToEmail}, with Subject: {mailRequest.Subject}, sended successfully." };
            await logEndPoint.Send(logEntity);
        }
    }
}
