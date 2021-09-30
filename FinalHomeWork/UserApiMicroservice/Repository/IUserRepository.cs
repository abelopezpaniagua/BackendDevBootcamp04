using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApiMicroservice.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByNickname(string nickname);
        public Task<List<User>> SearchUsersByNicknameOrFullName(string search);
        public Task<bool> IncrementUserTotalAttendance(int id);
        public Task<bool> DecrementUserTotalAttendance(int id);
        public Task<User> Authenticate(string nickname, string password);
    }
}
