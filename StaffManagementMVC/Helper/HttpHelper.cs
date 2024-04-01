namespace StaffManagementMVC.Helper
{
    public class HttpHelper
    {
        private static readonly string _serverName = "http://localhost:8081";

        internal static class Urls
        {
            public readonly static string GetStaff = $"{_serverName}/api/Staff";
            public readonly static string AddStaff = $"{_serverName}/api/Staff/Add";
            public readonly static string UpdateStaff = $"{_serverName}/api/Staff/Update";
            public readonly static string DeleteStaff = $"{_serverName}/api/Staff/Delete";
            public readonly static string SearchStaff = $"{_serverName}/api/Staff/Search";
        }

    }
}
