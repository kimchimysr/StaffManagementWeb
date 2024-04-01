namespace StaffManagementWebAPI.Models
{
	public interface IStaffQueryCriteria
	{
		string StaffId { get; set; }
		int Gender { get; set; }
		DateTime BirthdayFromDate { get; set; }
		DateTime BirthdayToDate { get; set; }
	}
}
