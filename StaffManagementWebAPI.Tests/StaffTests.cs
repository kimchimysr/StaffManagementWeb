using StaffManagementWebAPI.Models;
using StaffManagementWebAPI.Utilities;

namespace StaffManagementWebAPI.Tests
{
	public class StaffTests
	{
		[Fact]
		public void ShouldPassValidation_WhenModelIsValid()
		{
			var model = new Staff() { StaffId = "ST001", FullName = "Han Dara", Birthday = new DateTime(1998, 12, 12), Gender = 1 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 0);
		}

		[Fact]
		public void ShouldPassValidation_WhenBirthdayIsEmpty()
		{
			var model = new Staff() { StaffId = "ST001", FullName = "Han Dara", Gender = 1 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.Equal(new DateTime(0001, 01, 01), model.Birthday);
			Assert.True(validationResult.Count == 0);
		}

		[Fact]
		public void ShouldNotPassValidation_WhenStaffIdIsEmpty()
		{
			var model = new Staff() { FullName = "Han Dara", Birthday = new DateTime(1998, 12, 12), Gender = 1 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 1);
			Assert.Equal("The StaffId field is required.", validationResult[0].ErrorMessage);
		}

		[Fact]
		public void ShouldNotPassValidation_WhenStaffIdIsMoreThan8Characters()
		{
			var model = new Staff() { StaffId = "ST0012345", FullName = "Han Dara", Birthday = new DateTime(1998, 12, 12), Gender = 1 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 1);
			Assert.Equal("Staff ID cannot exceed 8 characters.", validationResult[0].ErrorMessage);
		}

		[Fact]
		public void ShouldNotPassValidation_WhenFullNameIsEmpty()
		{
			var model = new Staff() { StaffId = "ST001", Birthday = new DateTime(1998, 12, 12), Gender = 1 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 1);
			Assert.Equal("The FullName field is required.", validationResult[0].ErrorMessage);
		}

		[Fact]
		public void ShouldNotPassValidation_WhenFullNameIsMoreThan100Characters()
		{
			var name = "";
			for (int i = 0; i < 50; i++)
			{
				name += "Janet";
			}
			name += "L";

			var model = new Staff() { StaffId = "ST001", FullName = name, Birthday = new DateTime(1998, 12, 12), Gender = 1 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 1);
			Assert.Equal("Full Name cannot exceed 100 characters.", validationResult[0].ErrorMessage);
		}

		[Fact]
		public void ShouldNotPassValidation_WhenGenderIsEmpty()
		{
			var model = new Staff() { StaffId = "ST001", FullName = "Han Dara", Birthday = new DateTime(1998, 12, 12) };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 1);
			Assert.Equal("Gender must be 1(Male) or 2(Female).", validationResult[0].ErrorMessage);
		}

		[Fact]
		public void ShouldNotPassValidation_WhenGenderIsNot1Or2()
		{
			var model = new Staff() { StaffId = "ST001", FullName = "Han Dara", Birthday = new DateTime(1998, 12, 12), Gender = 3 };
			var validationResult = ModelValidatorHelper.ValidateModel(model);
			Assert.True(validationResult.Count == 1);
			Assert.Equal("Gender must be 1(Male) or 2(Female).", validationResult[0].ErrorMessage);
		}
	}
}