using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice.Repository
{
    public interface ICampaignRepository
    {
        public void SaveCampaign(Campaign campaign);
    }
}
