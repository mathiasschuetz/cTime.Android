using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cTime.Android.Core.Data;
using Time = Java.Sql.Time;

namespace cTime.Android.Core.Services.CTime
{
    public class CTimeService : ICTimeService
    {
        public Task<User> Login(string companyId, string emailAddress, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Time>> GetTimes(string employeeGuid, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveTimer(string employeeGuid, DateTime time, string companyId, TimeState state)
        {
            throw new NotImplementedException();
        }

        public Task<Time> GetCurrentTime(string employeeGuid)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AttendingUser>> GetAttendingUsers(string companyId)
        {
            throw new NotImplementedException();
        }
    }
}