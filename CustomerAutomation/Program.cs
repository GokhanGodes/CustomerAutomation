using CustomerAutomation.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Customers.Add(new Models.Customer { Id = 1, TCKN = "28727133856", Name = "Gökhan", LastName = "Gödeþ", BirthDate = DateTime.Parse("12.06.1995"), PhoneNumber = "05050911295", Address = "Ýzmir", IsActive = true });
                context.Customers.Add(new Models.Customer { Id = 2, TCKN = "28727133858", Name = "Doðukan", LastName = "Gödeþ", BirthDate = DateTime.Parse("16.07.2001"), PhoneNumber = "05061782420", Address = "Ankara", IsActive = true });
                context.Customers.Add(new Models.Customer { Id = 3, TCKN = "28727133860", Name = "Elif Öykü", LastName = "Gödeþ", BirthDate = DateTime.Parse("03.08.2012"), PhoneNumber = "-", Address = "Ýstanbul", IsActive = true });

                context.SaveChanges();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
