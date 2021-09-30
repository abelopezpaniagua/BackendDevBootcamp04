using Domain.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StatsApiMicroservice.Consumers
{
    public class DeleteUserConsumer : IConsumer<User>
    {
        public async Task Consume(ConsumeContext<User> context)
        {
            var userId = context.Message.Id;

            using (var httpClient = new HttpClient())
            {
                var deleteUserAttendancesUri = new Uri($"https://localhost:5003/api/attendance/users/{userId}/attendances");

                var response = await httpClient.DeleteAsync(deleteUserAttendancesUri);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
