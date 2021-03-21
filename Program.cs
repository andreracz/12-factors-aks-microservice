using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.CommandLine;
using System.CommandLine.Invocation;
using aks_12_factors_microservice.Repository;
using Microsoft.Extensions.DependencyInjection;
using Z.EntityFramework.Plus;

namespace aks_12_factors_microservice
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {
			var rootCommand = new RootCommand
			{

				new Option<bool>(
					"--migrate",
					getDefaultValue: () => false,
					"Run DB Migrations. Should be run as a Kubernetes Job"),
				new Option<bool>(
					"--purgeComplete",
					getDefaultValue: () => false,
					"Delete completed tasks. Should be run as a Kubernetes Job"),										
			};

			rootCommand.Handler = CommandHandler.Create<bool, bool>(async (bool migrate, bool purgeComplete) => {
				if (migrate) {
					IHost host = CreateHostBuilder(args).Build();
					using (var scope = host.Services.CreateScope())
					{
						var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
						db.Database.Migrate();
					}
				} else if (purgeComplete) {
					IHost host = CreateHostBuilder(args).Build();
					using (var scope = host.Services.CreateScope())
					{
						var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
						db.Todos.Where(todo => todo.Complete == true).Delete();
						db.SaveChanges();
					}
				} else {
					CreateHostBuilder(args).Build().Run();
				}
			});

			return rootCommand.InvokeAsync(args).Result;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
			return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
					
                });
		} 

        
	}
}
