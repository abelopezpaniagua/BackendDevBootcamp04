using AttendanceApiMicroservice.DbContext;
using Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApiMicroservice.Services
{
    public class UserAttendanceService : IUserAttendanceService
    {
        private readonly IMongoCollection<UserAttendance> _userAttendances;

        public UserAttendanceService(IAttendanceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userAttendances = database.GetCollection<UserAttendance>(settings.UserAttendanceCollectionName);
        }

        public List<UserAttendance> Get()
        {
            return this._userAttendances.AsQueryable().ToList();
        }

        public List<UserAttendance> GetByUserId(int userId)
        {
            return this._userAttendances
                .Find(f => f.UserId == userId)
                .ToList();
        }

        public UserAttendance Get(string id)
        {
            return this._userAttendances
                .Find<UserAttendance>(userAttendance => userAttendance.Id == id)
                .FirstOrDefault();
        }

        public UserAttendance Create(UserAttendance userAttendance)
        {
            this._userAttendances.InsertOne(userAttendance);
            return userAttendance;
        }

        public void Update(string id, UserAttendance userAttendanceIn)
        {
            this._userAttendances
                .ReplaceOne(userAttendance => userAttendance.Id == id, userAttendanceIn);
        }

        public void Remove(UserAttendance userAttendanceIn)
        {
            this._userAttendances
                .DeleteOne(userAttendance => userAttendance.Id == userAttendanceIn.Id);
        }

        public void Remove(string id)
        {
            this._userAttendances
                .DeleteOne(userAttendance => userAttendance.Id == id);
        }

        public void RemoveByUserId(int userId)
        {
            this._userAttendances
                .DeleteMany(userAttendance => userAttendance.UserId == userId);
        }
    }
}
