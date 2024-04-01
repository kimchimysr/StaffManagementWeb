﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StaffManagementMVC.Models
{
	public class Staff
	{
		[Required]
		[DisplayName("Staff ID")]
		[MaxLength(8, ErrorMessage = "Staff ID cannot exceed 8 characters.")]
		public string StaffId { get; set; } = string.Empty;

		[Required]
		[DisplayName("Full Name")]
		[MaxLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
		public string FullName { get; set; } = string.Empty;

		[Required]
        public DateTime Birthday { get; set; }

		[Required]
		[RegularExpression(@"^[12]$", ErrorMessage = "Gender must be 1(Male) or 2(Female).")]
		public int Gender { get; set; } // 1 = Male, 2 = Female

	}
}
