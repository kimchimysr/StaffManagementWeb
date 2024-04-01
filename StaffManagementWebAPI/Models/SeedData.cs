using Microsoft.EntityFrameworkCore;

namespace StaffManagementWebAPI.Models
{
	public class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			AppDbContext context = app.ApplicationServices
				.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			if (!context.Staff.Any())
			{
				context.Staff.AddRange(
					new Staff { StaffId = "ST001", FullName = "Fay Gockeler", Birthday = new DateTime(2001, 03, 17), Gender = 1 },
					new Staff { StaffId = "ST002", FullName = "Traver Steers", Birthday = new DateTime(2005, 05, 09), Gender = 1 },
					new Staff { StaffId = "ST003", FullName = "Ezmeralda Fewtrell", Birthday = new DateTime(2003, 10, 04), Gender = 2 },
					new Staff { StaffId = "ST004", FullName = "Emory Krzyzowski", Birthday = new DateTime(2000, 04, 08), Gender = 1 },
					new Staff { StaffId = "ST005", FullName = "Mela Fidgin", Birthday = new DateTime(2001, 09, 07), Gender = 2 },
					new Staff { StaffId = "ST006", FullName = "Allin Mapledoram", Birthday = new DateTime(2002, 12, 26), Gender = 1 },
					new Staff { StaffId = "ST007", FullName = "Doralia Pinnock", Birthday = new DateTime(2002, 10, 01), Gender = 2 },
					new Staff { StaffId = "ST008", FullName = "Ferdy Mellmoth", Birthday = new DateTime(2000, 10, 06), Gender = 1 },
					new Staff { StaffId = "ST009", FullName = "Quentin Gittoes", Birthday = new DateTime(2000, 06, 27), Gender = 2 },
					new Staff { StaffId = "ST010", FullName = "Oralla Normanvill", Birthday = new DateTime(2003, 03, 01), Gender = 2 },
					new Staff { StaffId = "ST011", FullName = "Katie Jenkins", Birthday = new DateTime(2002, 01, 20), Gender = 2 },
					new Staff { StaffId = "ST012", FullName = "Jacquetta Algar", Birthday = new DateTime(2000, 11, 17), Gender = 2 },
					new Staff { StaffId = "ST013", FullName = "Gilburt Maxworthy", Birthday = new DateTime(2005, 03, 18), Gender = 1 },
					new Staff { StaffId = "ST014", FullName = "Lenore Avramovitz", Birthday = new DateTime(2005, 05, 02), Gender = 2 },
					new Staff { StaffId = "ST015", FullName = "Hardy Stetson", Birthday = new DateTime(2003, 06, 23), Gender = 1 },
					new Staff { StaffId = "ST016", FullName = "Cointon Leece", Birthday = new DateTime(2003, 04, 19), Gender = 1 },
					new Staff { StaffId = "ST017", FullName = "Adriena Bebb", Birthday = new DateTime(2002, 08, 21), Gender = 2 },
					new Staff { StaffId = "ST018", FullName = "Herbert Wilmut", Birthday = new DateTime(2005, 04, 20), Gender = 1 },
					new Staff { StaffId = "ST019", FullName = "Enrico Dooland", Birthday = new DateTime(2002, 04, 01), Gender = 1 },
					new Staff { StaffId = "ST020", FullName = "Fonzie Maly", Birthday = new DateTime(2000, 04, 09), Gender = 1 },
					new Staff { StaffId = "ST021", FullName = "Adrianna Rosnau", Birthday = new DateTime(2001, 09, 06), Gender = 2 },
					new Staff { StaffId = "ST022", FullName = "Clari Hyde", Birthday = new DateTime(2003, 09, 11), Gender = 2 },
					new Staff { StaffId = "ST023", FullName = "Ailee Connue", Birthday = new DateTime(2005, 02, 27), Gender = 2 },
					new Staff { StaffId = "ST024", FullName = "Arlene Tertre", Birthday = new DateTime(2004, 08, 28), Gender = 2 },
					new Staff { StaffId = "ST025", FullName = "Amanda Yonnie", Birthday = new DateTime(2001, 06, 12), Gender = 2 },
					new Staff { StaffId = "ST026", FullName = "Mayer Danford", Birthday = new DateTime(2003, 08, 15), Gender = 1 },
					new Staff { StaffId = "ST027", FullName = "Kahlil Oller", Birthday = new DateTime(2001, 04, 23), Gender = 1 },
					new Staff { StaffId = "ST028", FullName = "Stern Watkiss", Birthday = new DateTime(2000, 04, 08), Gender = 1 },
					new Staff { StaffId = "ST029", FullName = "Katleen Melloy", Birthday = new DateTime(2002, 01, 25), Gender = 2 },
					new Staff { StaffId = "ST030", FullName = "Pattie Salisbury", Birthday = new DateTime(2003, 07, 08), Gender = 1 },
					new Staff { StaffId = "ST031", FullName = "Alexia Dunbavin", Birthday = new DateTime(2001, 08, 17), Gender = 2 },
					new Staff { StaffId = "ST032", FullName = "Papagena Mussen", Birthday = new DateTime(2001, 12, 02), Gender = 2 },
					new Staff { StaffId = "ST033", FullName = "Ron Witts", Birthday = new DateTime(2001, 02, 27), Gender = 1 },
					new Staff { StaffId = "ST034", FullName = "Hailey Lelliott", Birthday = new DateTime(2004, 06, 28), Gender = 1 },
					new Staff { StaffId = "ST035", FullName = "Salaidh Rathke", Birthday = new DateTime(2004, 07, 09), Gender = 2 },
					new Staff { StaffId = "ST036", FullName = "Patrizius Killen", Birthday = new DateTime(2004, 10, 29), Gender = 1 },
					new Staff { StaffId = "ST037", FullName = "Clarie Broker", Birthday = new DateTime(2000, 11, 21), Gender = 2 },
					new Staff { StaffId = "ST038", FullName = "Sylvester Fendlow", Birthday = new DateTime(2002, 05, 14), Gender = 1 },
					new Staff { StaffId = "ST039", FullName = "Bowie Quaintance", Birthday = new DateTime(2000, 11, 21), Gender = 1 },
					new Staff { StaffId = "ST040", FullName = "Malena Matejic", Birthday = new DateTime(2001, 06, 28), Gender = 2 },
					new Staff { StaffId = "ST041", FullName = "Germain Bonellie", Birthday = new DateTime(2000, 08, 30), Gender = 2 },
					new Staff { StaffId = "ST042", FullName = "Doralyn Whittlesey", Birthday = new DateTime(2003, 06, 23), Gender = 2 },
					new Staff { StaffId = "ST043", FullName = "Esdras Polendine", Birthday = new DateTime(2004, 08, 21), Gender = 1 },
					new Staff { StaffId = "ST044", FullName = "Aggi Fretwell", Birthday = new DateTime(2001, 02, 03), Gender = 2 },
					new Staff { StaffId = "ST045", FullName = "Allis Heisham", Birthday = new DateTime(2004, 02, 03), Gender = 2 },
					new Staff { StaffId = "ST046", FullName = "Rozanna Siddell", Birthday = new DateTime(2005, 10, 25), Gender = 2 },
					new Staff { StaffId = "ST047", FullName = "Alia Picard", Birthday = new DateTime(2005, 10, 09), Gender = 2 },
					new Staff { StaffId = "ST048", FullName = "Ashbey Gyde", Birthday = new DateTime(2001, 07, 27), Gender = 1 },
					new Staff { StaffId = "ST049", FullName = "Locke Gwilt", Birthday = new DateTime(2000, 11, 17), Gender = 1 },
					new Staff { StaffId = "ST050", FullName = "Joan Gooderson", Birthday = new DateTime(2002, 06, 20), Gender = 2 }
				);

				context.SaveChanges();
			}
		}

	}
}
