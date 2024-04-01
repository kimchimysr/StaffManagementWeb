using Microsoft.EntityFrameworkCore;
using StaffManagementWebAPI.Models;

namespace StaffManagementWebAPI.Repositories
{
	public class StaffRepository : IStaffRepository
	{
		public IQueryable<Staff> Staffs => _context.Staff;
		private AppDbContext _context;

		public StaffRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddStaffAsync(Staff staff)
		{
			await _context.Staff.AddAsync(staff);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateStaffAsync(Staff staff)
		{
			var shiftToUpdate = await _context.Staff.FindAsync(staff.StaffId);
			if (shiftToUpdate == null)
				throw new Exception("Staff Not Found.");

			_context.Entry(shiftToUpdate).CurrentValues.SetValues(staff);
			_context.Entry(shiftToUpdate).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteStaffAsync(string id)
		{
			var staff = await _context.Staff.FindAsync(id);
			if (staff == null)
				throw new Exception("Staff Not Found.");

			_context.Staff.Remove(staff);
			await _context.SaveChangesAsync();
		}

	}
}
