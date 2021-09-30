using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApiMicroservice.Services
{
    public interface IUserAttendanceService
    {
        public List<UserAttendance> Get();
        public List<UserAttendance> GetByUserId(int userId);
        public UserAttendance Get(string id);
        public UserAttendance Create(UserAttendance userAttendance);
        public void Update(string id, UserAttendance userAttendanceIn);
        public void Remove(UserAttendance userAttendanceIn);
        public void Remove(string id);
        public void RemoveByUserId(int userId);
    }
}
