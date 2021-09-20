using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CampaignRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void SaveCampaign(Campaign campaign)
        {
            this.applicationDbContext.Campaigns.Add(campaign);
            this.applicationDbContext.SaveChanges();
        }
    }
}
