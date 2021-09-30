using Domain;
using Domain.Models;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UserApiMicroservice.Repository;

namespace UserApiMicroservice.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IBus bus, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._bus = bus;
            this._configuration = configuration;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await this._userRepository.GetAllAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            var existingUser = await this._userRepository.GetByIdAsync(id);

            if (existingUser != null)
            {
                existingUser.Attendances = await this.GetUserAttendances(existingUser.Id);
            }

            return existingUser;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var userIsCreated = await this._userRepository.CreateAsync(user);
            if (userIsCreated)
            {
                var userCreated = await this._userRepository
                    .GetUserByNickname(user.Nickname);
                return userCreated;
            }

            return null;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var userIsUpdated = await this._userRepository.UpdateAsync(user);
            return userIsUpdated;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var userIsDeleted = await this._userRepository.DeleteAsync(user);

            if (userIsDeleted)
            {
                await this.SendUserDeleteQueue(user);   
            }

            return userIsDeleted;
        }

        public async Task<User> GetUserByNickname(string nickname)
        {
            var existingUser = await this._userRepository
                .GetUserByNickname(nickname);
            return existingUser;
        }

        public async Task<List<User>> SearchUsersByNicknameOrFullName(string search)
        {
            return await this._userRepository
                .SearchUsersByNicknameOrFullName(search);
        }

        public async Task<bool> IncrementUserTotalAttendance(User user)
        {
            var userAttendanceIsUpdated = await this._userRepository
                .IncrementUserTotalAttendance(user.Id);

            return userAttendanceIsUpdated;
        }

        public async Task<bool> DecrementUserTotalAttendance(User user)
        {
            var userAttendanceIsUpdated = await this._userRepository
                .DecrementUserTotalAttendance(user.Id);

            return userAttendanceIsUpdated;
        }

        public Task<User> Authenticate(string nickname, string password)
        {
            return this._userRepository.Authenticate(nickname, password);
        }

        private async Task<List<UserAttendance>> GetUserAttendances(int userId)
        {
            using (var httpClient = new HttpClient())
            {
                var getUserAttendancesUri = new Uri($"https://localhost:5003/api/attendance/users/{userId}/attendances");
                var response = await httpClient.GetAsync(getUserAttendancesUri);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var attendances = JsonSerializer.Deserialize<List<UserAttendance>>(
                    content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return attendances;
            }
        }

        private async Task SendUserDeleteQueue(User user)
        {
            Uri userDeleteUri = new Uri($"{_configuration.GetConnectionString("rabbitmq")}/{RabbitMqConsts.DeleteUserServiceQueue}");
            var userDeleteUriEndpoint = await this._bus.GetSendEndpoint(userDeleteUri);

            await userDeleteUriEndpoint.Send(user);
        }
    }
}
