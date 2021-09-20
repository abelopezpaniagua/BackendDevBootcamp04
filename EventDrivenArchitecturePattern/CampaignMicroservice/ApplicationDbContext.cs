using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignMicroservice
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Campaign> Campaigns { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }
    }
}
