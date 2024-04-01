using Newtonsoft.Json;
using System.Text;

namespace StaffManagementWebAPI.Tests.Helper
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }
        internal static class Urls
        {
            public readonly static string GetStaff = "/api/Staff/";
            public readonly static string AddStaff = "/api/Staff/Add";
            public readonly static string UpdateStaff = "/api/Staff/Update";
            public readonly static string DeleteStaff = "/api/Staff/Delete/";
            public readonly static string SearchStaff = "/api/Staff/Search";
        }
    }
}
