using APIGateway.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIGateway.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthToken>> GetAuthentication([FromBody] AuthUser user)
        {
            using (var httpClient = new HttpClient())
            {
                var contentSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                var content = new StringContent(contentSerialized, Encoding.UTF8, "application/json");

                var userAuthenticatedUri = new Uri($"http://localhost:5024/api/user/auth");
                var response = await httpClient.PostAsync(userAuthenticatedUri, content);

                response.EnsureSuccessStatusCode();

                var contentReaded = await response.Content.ReadAsStringAsync();
                var userAuthenticated = JsonSerializer.Deserialize<User>(
                    contentReaded,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (user.Username != userAuthenticated.Nickname)
                {
                    return BadRequest(new { Message = "Incorrect credentials." });
                }

                user.userId = userAuthenticated.Id;
            }

            return new UsersApiTokenService().GenerateToken(user);
        }
    }
}
