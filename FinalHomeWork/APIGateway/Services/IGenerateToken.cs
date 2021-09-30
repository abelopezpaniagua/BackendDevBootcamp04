using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    public interface IGenerateToken
    {
        public AuthToken GenerateToken(AuthUser user);
    }
}
