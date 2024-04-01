using StaffManagementMVC.Models;

namespace StaffManagementMVC.Helper
{
	public class AdvanceSearchHelper
	{
		public static string GenerateSearchStaffUrl(StaffQueryCriteria? query)
		{
			// Get All Staff
			string url = $"{HttpHelper.Urls.GetStaff}/0";

			if (query != null)
			{
				var queryParams = new List<string>();
				if (query.StaffId != null)
				{
					queryParams.Add($"StaffId={query.StaffId}");
				}
				if (query.Gender != null)
				{
					queryParams.Add($"Gender={query.Gender}");
				}
				if (query.BirthdayFromDate != null)
				{
					queryParams.Add($"BirthdayFromDate={query.BirthdayFromDate?.ToString("yyyy-MM-dd")}");
				}
				if (query.BirthdayToDate != null)
				{
					queryParams.Add($"BirthdayToDate={query.BirthdayToDate?.ToString("yyyy-MM-dd")}");
				}

				if (queryParams.Any())
				{
					url = $"{HttpHelper.Urls.SearchStaff}?{string.Join('&', queryParams)}";
				}
			}

			return url;

		}
	}
}
