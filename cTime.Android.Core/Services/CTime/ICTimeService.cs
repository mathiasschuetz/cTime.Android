using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cTime.Android.Core.Data;
using Time = Java.Sql.Time;

namespace cTime.Android.Core.Services.CTime
{
    public interface ICTimeService
    {
        Task<User> Login(string companyId, string emailAddress, string password);
        Task<IList<Time>> GetTimes(string employeeGuid, DateTime start, DateTime end);
        Task<bool> SaveTimer(string employeeGuid, DateTime time, string companyId, TimeState state);
        Task<Time> GetCurrentTime(string employeeGuid);
        Task<IList<AttendingUser>> GetAttendingUsers(string companyId);
    }
}