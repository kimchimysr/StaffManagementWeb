using StaffManagementWebAPI.Models;

namespace StaffManagementWebAPI.Repositories
{
	public interface IStaffRepository
	{
		IQueryable<Staff> Staffs { get; }

		Task AddStaffAsync(Staff staff);
		Task UpdateStaffAsync(Staff staff);
		Task DeleteStaffAsync(string id);
	}
}
