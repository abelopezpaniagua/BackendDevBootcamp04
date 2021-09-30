using Domain.Enums;
using Domain.Models;
using MassTransit;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StatsApiMicroservice.Consumers
{
    public class CreateUserAttendanceConsumer : IConsumer<UserAttendance>
    {
        public async Task Consume(ConsumeContext<UserAttendance> context)
        {
            var userId = context.Message.UserId;

            using (var httpClient = new HttpClient())
            {
                var updatedAttendance = new UserAttendanceUpdateForm()
                {
                    attendanceUpdateType = AttendanceUpdateType.IncrementAttendance
                };

                var contentSerialized = JsonConvert.SerializeObject(updatedAttendance);

                var content = new StringContent(contentSerialized, Encoding.UTF8, "application/json");

                var updateAttendanceUri = new Uri($"https://localhost:5023/api/user/{userId}/total-attendances");

                var response = await httpClient.PostAsync(updateAttendanceUri, content);
                
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
