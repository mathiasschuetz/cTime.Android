using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using cTime.Core.Data;
using cTime.Core.Results;
using Newtonsoft.Json.Linq;

namespace cTime.Core.Service
{
    public class CTimeService : ICTimeService
    {
        public async Task<ResultData<User>> Login(string emailAddress, string password)
        {
            try
            {
                var responseJson = await this.SendRequestAsync("GetRFIDbyLoginName.php", new Dictionary<string, string>
                {
                    {"Password", password},
                    {"LoginName", emailAddress}
                });

                var user = responseJson?
                    .Value<JArray>("Result")
                    .OfType<JObject>()
                    .FirstOrDefault();

                if (user == null)
                    return null;

                return Result<User>.Success(new User
                {
                    Id = user.Value<string>("EmployeeGUID"),
                    CompanyId = user.Value<string>("CompanyGUID"),
                    Email = user.Value<string>("LoginName"),
                    FirstName = user.Value<string>("EmployeeFirstName"),
                    Name = user.Value<string>("EmployeeName"),
                    ImageAsPng = Convert.FromBase64String(user.Value<string>("EmployeePhoto") ?? string.Empty)
                });
            }
            catch (Exception exception)
            {
                //throw new CTimeException(CTime2CoreResources.Get("CTimeService.ErrorWhileLogin") , exception);
                return Result<User>.Error(exception.Message);
            }
        }

        public async Task<ResultData<IList<Time>>> GetTimes(string employeeGuid, DateTime start, DateTime end)
        {
            try
            {
                var responseJson = await this.SendRequestAsync("GetTimeTrackList.php", new Dictionary<string, string>
                {
                    {"EmployeeGUID", employeeGuid},
                    {"DateTill", end.ToString("dd.MM.yyyy")},
                    {"DateFrom", start.ToString("dd.MM.yyyy")}
                });

                if (responseJson == null)
                    return Result<IList<Time>>.Success(new List<Time>());

                return Result<IList<Time>>.Success(responseJson
                    .Value<JArray>("Result")
                    .OfType<JObject>()
                    .Select(f => new Time
                    {
                        Day = f.Value<DateTime>("day"),
                        Hours =
                            TimeSpan.FromHours(double.Parse(f.Value<string>("DayHours") ?? "0",
                                CultureInfo.InvariantCulture)),
                        State =
                            f["TimeTrackType"].ToObject<int?>() == 0
                                ? null
                                : (TimeState?) f["TimeTrackType"].ToObject<int?>(),
                        ClockInTime = f["TimeTrackIn"].ToObject<DateTime?>(),
                        ClockOutTime = f["TimeTrackOut"].ToObject<DateTime?>()
                    })
                    .ToList());
            }
            catch (Exception exception)
            {
                //Logger.Error(exception, $"Exception in method {nameof(this.GetTimes)}. Employee: {employeeGuid}, Start: {start}, End: {end}");
                //throw new CTimeException(CTime2CoreResources.Get("CTimeService.ErrorWhileLoadingTimes"), exception);
                return Result<IList<Time>>.Error(exception.Message);
            }
        }

        public async Task<ResultData<bool>> SaveTimer(string employeeGuid, DateTime time, string companyId,
            TimeState state)
        {
            try
            {
                var responseJson = await this.SendRequestAsync("SaveTimer.php", new Dictionary<string, string>
                {
                    {"RFID", string.Empty},
                    {"TimerKind", ((int) state).ToString()},
                    {"TimerText", string.Empty},
                    {"TimerTime", time.ToString("yyyy-MM-dd HH:mm:ss")},
                    {"EmployeeGUID", employeeGuid},
                    {"GUID", companyId}
                });

                return Result<bool>.Success(responseJson?.Value<int>("State") == 0);
            }
            catch (Exception exception)
            {
                //Logger.Error(exception, $"Exception in method {nameof(this.SaveTimer)}. Employee: {employeeGuid}, Time: {time}, Company Id: {companyId}, State: {(int)state}");
                //throw new CTimeException(CTime2CoreResources.Get("CTimeService.ErrorWhileStamp"), exception);
                return Result<bool>.Error(exception.Message);
            }
        }

        public async Task<ResultData<Time>> GetCurrentTime(string employeeGuid)
        {
            try
            {
                var timesForToday = await this.GetTimes(employeeGuid, DateTime.Today, DateTime.Today.AddDays(1));

                var itemsToIgnore = timesForToday.Data?
                    .Where(f =>
                        (f.ClockInTime != null && f.ClockOutTime != null) ||
                        (f.ClockInTime == null && f.ClockOutTime == null))
                    .ToList();

                var latestFinishedTimeToday = itemsToIgnore
                    .Where(f => f.ClockInTime != null && f.ClockOutTime != null)
                    .OrderByDescending(f => f.ClockOutTime)
                    .FirstOrDefault();

                return
                    Result<Time>.Success(
                        timesForToday.Data?.FirstOrDefault(
                            f => itemsToIgnore != null && itemsToIgnore.Contains(f) == false) ??
                        latestFinishedTimeToday);
            }
            catch (Exception exception)
            {
                //Logger.Error(exception, $"Exception in method {nameof(this.GetCurrentTime)}. Employee: {employeeGuid}");
                //throw new CTimeException(CTime2CoreResources.Get("CTimeService.ErrorWhileLoadingCurrentTime"), exception);
                return Result<Time>.Error(exception.Message);
            }
        }

        public async Task<ResultData<IList<AttendingUser>>> GetAttendingUsers(string companyId)
        {
            try
            {
                var responseJson = await this.SendRequestAsync("GetPresenceList.php", new Dictionary<string, string>
                {
                    {"GUID", companyId}
                });

                if (responseJson == null)
                    return Result<IList<AttendingUser>>.Success(new List<AttendingUser>());

                return Result<IList<AttendingUser>>.Success(responseJson
                    .Value<JArray>("Result")
                    .OfType<JObject>()
                    .Select(f => new AttendingUser
                    {
                        Name = f.Value<string>("EmployeeName"),
                        FirstName = f.Value<string>("EmployeeFirstName"),
                        IsAttending = f.Value<int>("PresenceStatus") == 1,
                        ImageAsPng = Convert.FromBase64String(f.Value<string>("EmployeePhoto") ?? string.Empty)
                    })
                    .ToList());
            }
            catch (Exception exception)
            {
                //Logger.Error(exception, $"Exception in method {nameof(this.GetAttendingUsers)}. Company Id: {companyId}");
                //throw new CTimeException(CTime2CoreResources.Get("CTimeService.ErrorWhileLoadingAttendanceList"), exception);
                return Result<IList<AttendingUser>>.Error(exception.Message);
            }
        }

        private async Task<JObject> SendRequestAsync(string function, Dictionary<string, string> data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, BuildUri(function))
            {
                Content = new FormUrlEncodedContent(data)
            };

            var response = await new HttpClient().SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var responseContentAsString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(responseContentAsString);

            var responseState = responseJson.Value<int>("State");

            if (responseState != 0)
                return null;

            return responseJson;
        }

        private static Uri BuildUri(string path)
        {
            return new Uri($"https://app.c-time.net/php/{path}");
        }
    }
}