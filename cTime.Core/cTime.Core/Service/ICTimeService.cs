using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cTime.Core.Data;
using cTime.Core.Results;

namespace cTime.Core.Service
{
    public interface ICTimeService
    {
        Task<ResultData<User>> Login(string emailAddress, string password);
        Task<ResultData<IList<Time>>> GetTimes(string employeeGuid, DateTime start, DateTime end);
        Task<ResultData<bool>> SaveTimer(string employeeGuid, DateTime time, string companyId, TimeState state);
        Task<ResultData<Time>> GetCurrentTime(string employeeGuid);
        Task<ResultData<IList<AttendingUser>>> GetAttendingUsers(string companyId);
    }
}