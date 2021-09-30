using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UserApiMicroservice.Repository
{
    public class BaseRepository
    {
        private readonly IConfiguration configuration;

        protected BaseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
