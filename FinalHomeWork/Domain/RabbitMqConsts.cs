using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RabbitMqConsts
    {
        public const string Username = "guest";
        public const string Password = "guest";
        
        public const string UserServiceQueue = "user-service";

        public const string CreateAttendanceServiceQueue = "attendance-service.create";
        public const string DeleteAttendanceServiceQueue = "attendance-service.delete";

        public const string DeleteUserServiceQueue = "user-service.delete";
    }
}
