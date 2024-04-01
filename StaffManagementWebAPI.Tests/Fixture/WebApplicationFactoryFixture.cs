using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StaffManagementWebAPI.Models;

namespace StaffManagementWebAPI.Tests.Fixture
{
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        private const string _connectionString
            = @$"Server=.\MSSQLServer1;Database=UserIntegration;Trusted_Connection=True";
        private WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }
        public WebApplicationFactoryFixture()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(Services =>
                {
                    Services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                    Services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseSqlServer(_connectionString);
                    });
                });
            });
            Client = _factory.CreateClient();
        }

        public void SeedData()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<AppDbContext>();

                cntx.Database.EnsureCreated();
                var existingStaff = cntx.Staff.ToList();
                foreach (var staff in existingStaff)
                {
                    cntx.Staff.Remove(staff);
                }

                cntx.Staff.AddRange(
                   new Staff
                   {
                       StaffId = "ST001",
                       FullName = "Test1",
                       Birthday = new DateTime(1995, 01, 01),
                       Gender = 1
                   },
                   new Staff
                   {
                       StaffId = "ST002",
                       FullName = "Test2",
                       Birthday = new DateTime(1996, 01, 01),
                       Gender = 2
                   },
                   new Staff
                   {
                       StaffId = "ST003",
                       FullName = "Test3",
                       Birthday = new DateTime(1997, 01, 01),
                       Gender = 1
                   }
                   );
                cntx.SaveChanges();
            }

        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<AppDbContext>();

                await cntx.Database.EnsureDeletedAsync();
            }
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<AppDbContext>();

                await cntx.Database.EnsureCreatedAsync();
				var existingStaff = await cntx.Staff.ToListAsync();
				foreach (var staff in existingStaff)
				{
					cntx.Staff.Remove(staff);
				}


				await cntx.Staff.AddRangeAsync(
                    new Staff
                    {
                        StaffId = "ST001",
                        FullName = "Test1",
                        Birthday = new DateTime(1995, 01, 01),
                        Gender = 1
                    },
                    new Staff
                    {
                        StaffId = "ST002",
                        FullName = "Test2",
                        Birthday = new DateTime(1996, 01, 01),
                        Gender = 2
                    },
                    new Staff
                    {
                        StaffId = "ST003",
                        FullName = "Test3",
                        Birthday = new DateTime(1997, 01, 01),
                        Gender = 1
                    }
                    );

                await cntx.SaveChangesAsync();
            }
        }
    }
}
