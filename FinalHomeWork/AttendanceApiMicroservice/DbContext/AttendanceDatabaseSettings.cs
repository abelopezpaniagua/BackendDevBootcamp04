using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApiMicroservice.DbContext
{
    public class AttendanceDatabaseSettings : IAttendanceDatabaseSettings
    {
        public string UserAttendanceCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
