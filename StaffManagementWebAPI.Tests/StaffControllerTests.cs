using StaffManagementWebAPI.Models;
using System.Net.Http.Json;
using System.Net;
using StaffManagementWebAPI.Tests.Helper;
using StaffManagementWebAPI.Tests.Fixture;

namespace StaffManagementWebAPI.Tests
{
    public class StaffControllerTests : IClassFixture<WebApplicationFactoryFixture>
	{
		private readonly WebApplicationFactoryFixture _factory;
		public StaffControllerTests(WebApplicationFactoryFixture factory)
		{
			_factory = factory;
			_factory.SeedData();
		}

		[Fact]
		public async Task GetById_ReturnsStaff_WhenIdExists()
		{
			// Arrange
			var staffId = "ST001";

			// Act
			var response = await _factory.Client.GetAsync(HttpHelper.Urls.GetStaff + staffId);

			// Assert
			var staff = await response.Content.ReadFromJsonAsync<Staff>();

			if (staff == null)
				return;

			Assert.True(response.StatusCode == HttpStatusCode.OK);
			Assert.Equal("ST001", staff.StaffId);
			Assert.Equal("Test1", staff.FullName);
		}

		[Fact]
		public async Task GetById_ReturnsAllStaff_WhenNotSpecifyId()
		{
			// Arrange
			var staffId = "0";

			// Act
			var response = await _factory.Client.GetAsync(HttpHelper.Urls.GetStaff + staffId);

			// Assert
			var staffs = await response.Content.ReadFromJsonAsync<List<Staff>>();

			if (staffs == null)
				return;

			Assert.True(response.StatusCode == HttpStatusCode.OK);
			Assert.True(staffs.Count == 3);
			Assert.Equal("ST001", staffs[0].StaffId);
			Assert.Equal("ST002", staffs[1].StaffId);
			Assert.Equal("ST003", staffs[2].StaffId);
		}

		[Fact]
		public async Task GetById_ReturnNotFound_WhenCannotFoundSpecifiedId()
		{
			// Arrange
			var staffId = "ST004";

			// Act
			var response = await _factory.Client.GetAsync(HttpHelper.Urls.GetStaff + staffId);

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task SearchStaff_ReturnNotFound_WhenCannotFindStaff()
		{
			// Arrange
			var query = new StaffQueryCriteria()
			{
				StaffId = "ABC"
			};
			var url = HttpHelper.Urls.SearchStaff + $"?StaffId={query.StaffId}";

			// Act
			var response = await _factory.Client.GetAsync(url);

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task SearchStaff_ReturnOK_WhenFoundStaffByStaffId()
		{
			// Arrange
			var query = new StaffQueryCriteria()
			{
				StaffId = "ST002"
			};
			var url = HttpHelper.Urls.SearchStaff + $"?StaffId={query.StaffId}";

			// Act
			var response = await _factory.Client.GetAsync(url);
			var staff = await response.Content.ReadFromJsonAsync<List<Staff>>();
			if (staff == null)
				return;

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.OK);
			Assert.Equal(query.StaffId, staff[0].StaffId);
		}

		[Fact]
		public async Task SearchStaff_ReturnOK_WhenFoundStaffByGender()
		{
			// Arrange
			var query = new StaffQueryCriteria()
			{
				Gender = 1
			};
			var url = HttpHelper.Urls.SearchStaff + $"?Gender={query.Gender}";

			// Act
			var response = await _factory.Client.GetAsync(url);
			var staffs = await response.Content.ReadFromJsonAsync<List<Staff>>();
			if (staffs == null)
				return;

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.OK);
			Assert.Equal(query.Gender, staffs[0].Gender);
		}

		[Fact]
		public async Task SearchStaff_ReturnOK_WhenFoundStaffByBirthday()
		{
			// Arrange
			var query = new StaffQueryCriteria()
			{
				BirthdayFromDate = new DateTime(1996, 1, 1),
				BirthdayToDate = new DateTime(2000, 1, 1),
			};
			var url = HttpHelper.Urls.SearchStaff + $"?BirthdayFromDate={query.BirthdayFromDate}&BirthdayToDate={query.BirthdayToDate}";

			// Act
			var response = await _factory.Client.GetAsync(url);
			var staffs = await response.Content.ReadFromJsonAsync<List<Staff>>();
			if (staffs == null)
				return;

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.OK);
			Assert.True(staffs.Count == 2);
			Assert.True(staffs[0].Birthday >= query.BirthdayFromDate && staffs[0].Birthday <= query.BirthdayToDate);
			Assert.True(staffs[1].Birthday >= query.BirthdayFromDate && staffs[0].Birthday <= query.BirthdayToDate);
		}


		[Fact]
		public async Task CreateStaff_ReturnBadRequest_WhenModelDoesNotPassValidation()
		{
			// Arrange
			var staff = new Staff()
			{
				StaffId = "ST003",
				FullName = "Nara",
				Birthday = new DateTime(1995, 12, 12),
				// Only 1 or 2
				Gender = 3
			};

			var content = HttpHelper.GetJsonHttpContent(staff);

			// Act
			var response = await _factory.Client.PostAsync(HttpHelper.Urls.AddStaff, content);

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task CreateStaff_ReturnBadRequest_WhenStaffIdAlreadyExist()
		{
			// Arrange
			var staff = new Staff()
			{
				StaffId = "ST002",
				FullName = "Nara",
				Birthday = new DateTime(1995, 12, 12),
				Gender = 2
			};

			var content = HttpHelper.GetJsonHttpContent(staff);

			// Act
			var response = await _factory.Client.PostAsync(HttpHelper.Urls.AddStaff, content);

			// Assert
			Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
			var errorMessage = await response.Content.ReadAsStringAsync();
			Assert.Equal("Staff ID already exist!", errorMessage.ToString());
		}

		[Fact]
		public async Task CreateStaff_ReturnOK_WhenModelIsValid()
		{
			// Arrange
			var newStaff = new Staff()
			{
				StaffId = "ST004",
				FullName = "Nara",
				Birthday = new DateTime(1995, 12, 12),
				Gender = 2
			};
			var content = HttpHelper.GetJsonHttpContent(newStaff);

			// Act
			var request = await _factory.Client.PostAsync(HttpHelper.Urls.AddStaff, content);
			var response = await _factory.Client.GetAsync(HttpHelper.Urls.GetStaff + "ST004");
			var staff = await response.Content.ReadFromJsonAsync<Staff>();


			// Assert
			Assert.True(request.StatusCode == HttpStatusCode.OK);
			Assert.NotNull(staff);
			Assert.Equal(newStaff.StaffId, staff.StaffId);
			Assert.Equal(newStaff.FullName, staff.FullName);
			Assert.Equal(newStaff.Birthday, staff.Birthday);
			Assert.Equal(newStaff.Gender, staff.Gender);
		}


		[Fact]
		public async Task UpdateStaff_ReturnBadRequest_WhenModelDoesNotPassValidation()
		{
			// Arrange
			var staff = new Staff()
			{
				StaffId = "ST004",
				Birthday = new DateTime(1995, 12, 12),
				Gender = 1
			};
			var content = HttpHelper.GetJsonHttpContent(staff);

			// Act
			var request = await _factory.Client.PutAsync(HttpHelper.Urls.UpdateStaff, content);

			// Assert
			Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task UpdateStaff_ReturnBadRequest_WhenStaffIdDoesNotExist()
		{
			// Arrange
			var staff = new Staff()
			{
				StaffId = "ST009",
				FullName = "Nara",
				Birthday = new DateTime(1995, 12, 12),
				Gender = 1
			};
			var content = HttpHelper.GetJsonHttpContent(staff);

			// Act
			var request = await _factory.Client.PutAsync(HttpHelper.Urls.UpdateStaff, content);

			// Assert
			Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
			var errorMessage = await request.Content.ReadAsStringAsync();
			Assert.Equal("Staff ID does not exist!", errorMessage.ToString());
		}

		[Fact]
		public async Task UpdateStaff_ReturnOkRequest_WhenModelIsValid()
		{
			// Arrange
			var staff = new Staff()
			{
				StaffId = "ST002",
				FullName = "Nara",
				Birthday = new DateTime(1995, 12, 12),
				Gender = 1
			};
			var content = HttpHelper.GetJsonHttpContent(staff);

			// Act
			var request = await _factory.Client.PutAsync(HttpHelper.Urls.UpdateStaff, content);
			var response = await _factory.Client.GetAsync(HttpHelper.Urls.GetStaff + "ST002");
			var updatedstaff = await response.Content.ReadFromJsonAsync<Staff>();
			if (updatedstaff == null)
				return;

			// Assert
			Assert.True(request.StatusCode == HttpStatusCode.OK);
			Assert.Equal(updatedstaff.StaffId, staff.StaffId);
			Assert.Equal(updatedstaff.FullName, staff.FullName);
			Assert.Equal(updatedstaff.Birthday, staff.Birthday);
			Assert.Equal(updatedstaff.Gender, staff.Gender);
		}

		[Fact]
		public async Task DeleteStaff_ReturnBadRequest_WhenStaffIdDoesNotExist()
		{
			// Arrange
			var staffId = "ST009";

			// Act
			var request = await _factory.Client.DeleteAsync(HttpHelper.Urls.DeleteStaff + staffId);

			// Assert
			Assert.True(request.StatusCode == HttpStatusCode.BadRequest);
			var errorMessage = await request.Content.ReadAsStringAsync();
			Assert.Equal("Staff ID does not exist!", errorMessage.ToString());
		}

		[Fact]
		public async Task DeleteStaff_ReturnOkRequest_WhenStaffIdExist()
		{
			// Arrange
			var staffId = "ST003";

			// Act
			var request = await _factory.Client.DeleteAsync(HttpHelper.Urls.DeleteStaff + staffId);
			var response = await _factory.Client.GetAsync(HttpHelper.Urls.GetStaff + "ST003");

			// Assert
			Assert.True(request.StatusCode == HttpStatusCode.OK);
			Assert.True(response.StatusCode == HttpStatusCode.NotFound);
		}
	}
}
