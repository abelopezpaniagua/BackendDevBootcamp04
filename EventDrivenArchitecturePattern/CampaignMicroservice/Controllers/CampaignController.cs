using CampaignMicroservice.Repository;
using Domain;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CampaignMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly ICampaignRepository _campaignRepostory;
        private readonly IConfiguration _configuration;

        public CampaignController(IBus bus, ICampaignRepository campaignRepostory, IConfiguration configuration)
        {
            _bus = bus;
            _campaignRepostory = campaignRepostory;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign(Campaign campaign)
        {
            if (campaign != null)
            {
                this._campaignRepostory.SaveCampaign(campaign);
                
                Uri campaignUri = new Uri($"{_configuration.GetConnectionString("rabitmq")}/CampaignQueue");
                var campaignEndPoint = await _bus.GetSendEndpoint(campaignUri);
                await campaignEndPoint.Send(campaign);

                Uri logUri = new Uri($"{_configuration.GetConnectionString("rabitmq")}/LogQueue");
                var logEndPoint = await _bus.GetSendEndpoint(logUri);
                var logEntity = new LogMessage() { Description = $"Campaign: {campaign.Name}, created successfully." };
                await logEndPoint.Send(logEntity);

                return Created("", campaign);
            }
            return BadRequest();
        }
    }
}
