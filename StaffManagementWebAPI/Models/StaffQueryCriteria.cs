
namespace StaffManagementWebAPI.Models
{
	public class StaffQueryCriteria : IStaffQueryCriteria
	{
		public string StaffId { get; set; } = string.Empty;
		public int Gender { get; set; } = 0;
		public DateTime BirthdayFromDate { get; set; } = DateTime.MinValue;
		public DateTime BirthdayToDate { get; set; } = DateTime.MinValue;
	}
}
