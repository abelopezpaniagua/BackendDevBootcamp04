using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApiMicroservice.DbContext
{
    public interface IAttendanceDatabaseSettings
    {
        string UserAttendanceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
