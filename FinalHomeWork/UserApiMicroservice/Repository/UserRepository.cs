using Dapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UserApiMicroservice.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<List<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";

            try
            {
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<User>(query)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = CreateConnection())
            {
                return (await connection.QueryFirstOrDefaultAsync<User>(query, parameters));
            }
        }

        public async Task<bool> CreateAsync(User entity)
        {
            try
            {
                var query = "INSERT INTO Users (Nickname, FullName, Credentials, TotalAttendance) VALUES (@Nickname, @FullName, @Credentials, @TotalAttendance)";

                var parameters = new DynamicParameters();
                parameters.Add("Nickname", entity.Nickname, DbType.String);
                parameters.Add("FullName", entity.FullName, DbType.String);
                parameters.Add("Credentials", entity.Credentials, DbType.String);
                parameters.Add("TotalAttendance", entity.TotalAttendance, DbType.Int32);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters)) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            try
            {
                var query = "UPDATE Users SET Nickname = @Name, FullName = @FullName, Credentials = @Credentials WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Name", entity.Nickname, DbType.String);
                parameters.Add("FullName", entity.FullName, DbType.String);
                parameters.Add("Credentials", entity.Credentials, DbType.String);
                parameters.Add("Id", entity.Id, DbType.Int32);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters)) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteAsync(User entity)
        {
            try
            {
                var query = "DELETE FROM Users WHERE Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id, DbType.Int32);

                using (var connection = CreateConnection())
                {
                    return (await connection.ExecuteAsync(query, parameters)) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<User> GetUserByNickname(string nickname)
        {
            var query = "SELECT * FROM Users WHERE Nickname = @Nickname";

            var parameters = new DynamicParameters();
            parameters.Add("Nickname", nickname, DbType.String);

            using (var connection = CreateConnection())
            {
                return (await connection.QueryFirstOrDefaultAsync<User>(query, parameters));
            }
        }

        public async Task<List<User>> SearchUsersByNicknameOrFullName(string search)
        {
            var query = "SELECT * FROM Users WHERE Nickname LIKE @Search OR FullName LIKE @Search";

            try
            {
                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<User>(query, new { Search = "%" + search + "%" })).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> IncrementUserTotalAttendance(int id)
        {
            var query = "UPDATE Users SET TotalAttendance = TotalAttendance + 1 WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters) > 0;
                }
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> DecrementUserTotalAttendance(int id)
        {
            var query = "UPDATE Users SET TotalAttendance = TotalAttendance - 1 WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            try
            {
                using (var connection = CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<User> Authenticate(string nickname, string password)
        {
            var query = "SELECT * FROM Users WHERE Nickname = @Nickname AND Credentials = @Credentials";
            var parameters = new DynamicParameters();
            parameters.Add("Nickname", nickname, DbType.String);
            parameters.Add("Credentials", password, DbType.String);

            using (var connection = CreateConnection())
            {
                return (await connection.QueryFirstOrDefaultAsync<User>(query, parameters));
            }
        }
    }
}
