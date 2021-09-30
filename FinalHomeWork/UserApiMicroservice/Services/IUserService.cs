using Domain.Enums;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApiMicroservice.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task<User> CreateUserAsync(User user);
        public Task<bool> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(User user);
        public Task<User> GetUserByNickname(string nickname);
        public Task<List<User>> SearchUsersByNicknameOrFullName(string search);
        public Task<bool> IncrementUserTotalAttendance(User user);
        public Task<bool> DecrementUserTotalAttendance(User user);
        public Task<User> Authenticate(string nickname, string password);
    }
}
