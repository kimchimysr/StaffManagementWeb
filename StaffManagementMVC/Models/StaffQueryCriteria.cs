namespace StaffManagementMVC.Models
{
	public class StaffQueryCriteria : IStaffQueryCriteria
	{
		public string? StaffId { get; set; } = null;
		public int? Gender { get; set; } = null;
		public DateTime? BirthdayFromDate { get; set; } = null;
		public DateTime? BirthdayToDate { get; set; } = null;
	}
}
