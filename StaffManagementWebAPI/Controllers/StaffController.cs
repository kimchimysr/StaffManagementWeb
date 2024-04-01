using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffManagementWebAPI.Models;
using StaffManagementWebAPI.Repositories;

namespace StaffManagementWebAPI.Controllers
{
	[Route("api/Staff")]
	[ApiController]
	public class StaffController : ControllerBase
	{
		private IStaffRepository _staffRepository;
		public StaffController(IStaffRepository staffRepository)
		{
			_staffRepository = staffRepository;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetStaffByIdAsync([FromRoute] string id = "0")
		{
			if (id == "0")
				return Ok(await _staffRepository.Staffs.ToListAsync());

			var staff = await _staffRepository.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);

			if (staff == null)
				return NotFound();

			return Ok(staff);
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddStaffAsync([FromBody] Staff staff)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var existedStaff = await _staffRepository.Staffs.FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
			if (existedStaff != null)
				return BadRequest("Staff ID already exist!");

			await _staffRepository.AddStaffAsync(staff);
			return Ok();
		}

		[HttpPut("Update")]
		public async Task<IActionResult> UpdateStaffAsync([FromBody] Staff staff)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var existedStaff = await _staffRepository.Staffs.FirstOrDefaultAsync(s => s.StaffId == staff.StaffId);
			if (existedStaff == null)
				return BadRequest("Staff ID does not exist!");

			await _staffRepository.UpdateStaffAsync(staff);
			return Ok();
		}

		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteStaffAsync([FromRoute] string id)
		{
			var existedStaff = await _staffRepository.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
			if (existedStaff == null)
				return BadRequest("Staff ID does not exist!");

			await _staffRepository.DeleteStaffAsync(id);
			return Ok();
		}

		[HttpGet("Search")]
		public async Task<IActionResult> SearchStaffAsync([FromQuery] StaffQueryCriteria query)
		{
			var staffs = await _staffRepository.Staffs.Where
				(q => q.StaffId == (query.StaffId == string.Empty ? q.StaffId : query.StaffId)
				&& q.Gender == (query.Gender == 0 ? q.Gender : query.Gender)
				&& (q.Birthday >= (query.BirthdayFromDate == DateTime.MinValue ? q.Birthday : query.BirthdayFromDate)
				 && q.Birthday <= (query.BirthdayToDate == DateTime.MinValue ? q.Birthday : query.BirthdayToDate)
				)
			).ToListAsync();

			if (staffs.Count == 0)
				return NotFound();

			return Ok(staffs);
		}

	}
}
