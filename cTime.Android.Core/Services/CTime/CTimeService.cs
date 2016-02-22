using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using cTime.Android.Core.Data;
using Newtonsoft.Json;
using Time = Java.Sql.Time;
using Newtonsoft.Json.Linq;

namespace cTime.Android.Core.Services.CTime
{
    public class CTimeService : ICTimeService
    {
        public async Task<User> Login(string companyId, string emailAddress, string password)
        {
            try
            {
                var url = BuildUri("/ctimetrack/php/GetRFIDbyLoginName.php");
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET/POST";
                request.Headers.Add("Password", password);
                request.Headers.Add("LoginName", emailAddress);
                request.Headers.Add("GUIDlogin", companyId);
                request.ContentType = "application/json";

                string responseText;

                using (var response = await request.GetResponseAsync())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                        Console.WriteLine(responseText);
                    }
                }

                var responseJson = JObject.Parse(responseText);

                var state = responseJson.Value<int>("State");

                if (state != 0)
                    return null;

                var user = responseJson
                    .Value<JArray>("Result")
                    .OfType<JObject>()
                    .FirstOrDefault();

                if (user == null)
                    return null;

                return new User
                {
                    Id = user.Value<string>("EmployeeGUID"),
                    Email = user.Value<string>("LoginName"),
                    FirstName = user.Value<string>("EmployeeFirstName"),
                    Name = user.Value<string>("EmployeeName"),
                    ImageAsPng = Convert.FromBase64String(user.Value<string>("EmployeePhoto") ?? string.Empty),
                };
            }
            catch (Exception)
            {
                return null;
            }
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

        private static string BuildUri(string path)
        {
            return $"http://c-time.cloudapp.net{path}";
        }
    }
}