using ClosedXML.Excel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StaffManagementMVC.Helper;
using StaffManagementMVC.Models;
using System.Data;
using System.Text;

namespace StaffManagementMVC.Controllers
{
	public class StaffController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public StaffController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index(StaffQueryCriteria? query = null)
		{
			var client = _httpClientFactory.CreateClient();

			var url = AdvanceSearchHelper.GenerateSearchStaffUrl(query);

			var response = await client.GetAsync(url);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<List<Staff>>(jsonData);
				return View(result);
			}

			return View(null);
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Staff staff)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(staff);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync(HttpHelper.Urls.AddStaff, content);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			else
			{
				TempData["StaffIdExist"] = "Staff ID already exists. Please choose a unique ID.";
				return View(staff);
			}
		}

		public async Task<IActionResult> Edit(string id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"{HttpHelper.Urls.GetStaff}/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var data = JsonConvert.DeserializeObject<Staff>(jsonData);
				if (data == null)
					return View(null);
				return View(data);
			}

			return View(null);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Staff staff)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(staff);
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync(HttpHelper.Urls.UpdateStaff, content);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}

			return View(staff);
		}

		public async Task<IActionResult> Delete(string id)
		{
			var client = _httpClientFactory.CreateClient();
			await client.DeleteAsync($"{HttpHelper.Urls.DeleteStaff}/{id}");

			return RedirectToAction("Index");
		}

		public IActionResult Search()
		{
			return View();
		}

		[HttpGet]
		public async Task<FileResult> ExportStaffToExcel(StaffQueryCriteria? query)
		{
			var fileName = "StaffList.xlsx";

			var client = _httpClientFactory.CreateClient();
			var url = AdvanceSearchHelper.GenerateSearchStaffUrl(query);
			var response = await client.GetAsync(url);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<List<Staff>>(jsonData);
				if (result != null)
					return GenerateExcel(fileName, result);

			}
			return GenerateExcel(fileName, new List<Staff>());
		}

		private FileResult GenerateExcel(string fileName, IEnumerable<Staff> staffs)
		{
			DataTable dataTable = new DataTable("Staff");
			dataTable.Columns.AddRange(new DataColumn[]
			{
				new DataColumn("Staff ID"),
				new DataColumn("Full Name"),
				new DataColumn("Birthday"),
				new DataColumn("Gender")
			});

			foreach (var staff in staffs)
			{
				var birthday = staff.Birthday.ToString("dd/MM/yyyy");
				var gender = staff.Gender == 1 ? "Male" : "Female";

				dataTable.Rows.Add(staff.StaffId, staff.FullName, birthday, gender);
			}

			using (XLWorkbook wb = new XLWorkbook())
			{
				wb.Worksheets.Add(dataTable);
				using (MemoryStream stream = new MemoryStream())
				{
					wb.SaveAs(stream);

					return File(stream.ToArray(),
						"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
						fileName);
				}
			}
		}

		[HttpGet]
		public async Task<IActionResult> ExportStaffToPdf(StaffQueryCriteria? query)
		{
			var fileName = "StaffList.pdf";

			var client = _httpClientFactory.CreateClient();
			var url = AdvanceSearchHelper.GenerateSearchStaffUrl(query);
			var response = await client.GetAsync(url);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<List<Staff>>(jsonData);
				if (result != null)
					return File(GeneratePDF(result), "application/pdf", fileName);

			}
			return File(GeneratePDF(new List<Staff>()), "application/pdf", fileName);
		}

		private byte[] GeneratePDF(List<Staff> staffs)
		{
			//Define your memory stream which will temporarily hold the PDF
			using (MemoryStream stream = new MemoryStream())
			{
				//Initialize PDF writer
				PdfWriter writer = new PdfWriter(stream);
				//Initialize PDF document
				PdfDocument pdf = new PdfDocument(writer);
				// Initialize document
				Document document = new Document(pdf);
				// Add content to the document
				// Header
				document.Add(new Paragraph("Staff List")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(20));
				// Table for Staff
				Table table = new Table(new float[] { 3, 1, 1, 1 });
				table.SetWidth(UnitValue.CreatePercentValue(100));
				table.AddHeaderCell("Staff ID");
				table.AddHeaderCell("Full Name");
				table.AddHeaderCell("Birthday");
				table.AddHeaderCell("Gender");
				foreach (var staff in staffs)
				{
					var gender = staff.Gender == 1 ? "Male" : "Female";

					table.AddCell(new Cell().Add(new Paragraph(staff.StaffId)));
					table.AddCell(new Cell().Add(new Paragraph(staff.FullName.ToString())));
					table.AddCell(new Cell().Add(new Paragraph(staff.Birthday.ToString("dd/MM/yyyy"))));
					table.AddCell(new Cell().Add(new Paragraph(gender)));
				}
				//Add the Table to the PDF Document
				document.Add(table);
				// Close the Document
				document.Close();
				return stream.ToArray();
			}
		}
	}
}
